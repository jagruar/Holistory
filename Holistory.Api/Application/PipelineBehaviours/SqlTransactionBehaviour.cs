using Holistory.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.PipelineBehaviours
{
    public class SqlTransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlTransactionBehaviour(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default(TResponse);
            string typeName = typeof(TRequest).Name;

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                IExecutionStrategy strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using (IDbContextTransaction transaction = await _dbContext.BeginTransactionAsync())
                    {
                        response = await next();
                        await _dbContext.CommitTransactionAsync(transaction);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
