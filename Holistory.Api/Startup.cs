using AutoMapper;
using FluentValidation.AspNetCore;
using Holistory.Api.Application.MiddleWare;
using Holistory.Api.Application.PipelineBehaviours;
using Holistory.Api.Queries;
using Holistory.Api.Queries.Interfaces;
using Holistory.Api.Services.IdentityService;
using Holistory.Common.Configuration;
using Holistory.Common.Constants;
using Holistory.Common.Services;
using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Domain.Aggregates.UserAggregate;
using Holistory.Infrastructure.Sql;
using Holistory.Infrastructure.Sql.Repositories;
using Holistory.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;
using System.Text;

namespace Holistory.Api
{
    public class Startup
    {
        public const string CORS_POLICY = "AllowSpecificOrigins";

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CORS_POLICY,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                        builder.WithOrigins(Configuration.GetSection("CorsOrigins").Get<string[]>());
                    });
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc()
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                    configuration.ImplicitlyValidateChildProperties = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new Info { Title = "Holistory API", Version = "v1" });
                // string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                // swaggerOptions.IncludeXmlComments(xmlFilePath);
                swaggerOptions.AddFluentValidationRules();
                swaggerOptions.CustomSchemaIds(t => t.Name.Replace("Dto", string.Empty));
            });

            Assembly[] mediatrAssemblies = new Assembly[]
            {
                Assembly.Load("Holistory.Api.DomainEventHandlers"),
                Assembly.GetExecutingAssembly(),
            };

            services.AddMediatR(mediatrAssemblies);
            services.AddSignalR();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ApplicationDbContext>(
                dbContextOptions =>
                {
                    dbContextOptions.UseSqlServer(
                        Configuration.GetConnectionString("HolistoryDatabase"),
                        sqlOptions => sqlOptions.EnableRetryOnFailure(6, TimeSpan.FromSeconds(5), null));
                });

            // Add ASP.NET Identity services
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 5,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
            })
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add configuration options
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));

            // Add authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityRoles.Admin, policy =>
                    policy.RequireRole(IdentityRoles.Admin));

                options.AddPolicy(IdentityRoles.User, policy =>
                    policy.RequireRole(IdentityRoles.User));
            });

            // Add JWT bearer authentication
            JwtOptions jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = !HostingEnvironment.IsDevelopment();
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true
                    };
                });

            ConfigureDependencyInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseCors(CORS_POLICY);

            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "Portal.Api V1");
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            // queries
            services.AddScoped<ITopicQueries, TopicQueries>();

            // repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();

            // services
            services.AddScoped<IConnectionProvider, SqlServerConnectionProvider>();
            services.AddScoped<IIdentityService, IdentityService>();

            // pipelines
            // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SqlTransactionBehaviour<,>));
        }
    }
}
