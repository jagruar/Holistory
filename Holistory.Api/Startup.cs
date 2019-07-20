using AutoMapper;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Holistory.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using Holistory.Domain.Aggregates.AccountAggregate;
using Holistory.Infrastructure.Sql.Repositories;
using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Api.Application.PipelineBehaviours;
using Holistory.Api.Application.MiddleWare;
using Microsoft.AspNetCore.Identity;

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
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

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
            app.UseMvc();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();

            // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SqlTransactionBehaviour<,>));
        }
    }
}
