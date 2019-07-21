using Holistory.Domain.Aggregates.UserAggregate;
using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Domain.Seedwork;
using Holistory.Infrastructure.Sql.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Infrastructure.Sql
{
    public class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions options, IMediator mediator, IConfiguration configuration)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); ;
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Topic> Topic { get; set; }

        public bool HasActiveTransaction => CurrentTransaction != null;

        public IDbContextTransaction CurrentTransaction { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.DisableCascadingDeletes();

            base.OnModelCreating(modelBuilder);
        }

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            UpdateDeletedDate();

            await SaveChangesAsync();
        }

        /// <summary>
        /// Updates or adds any necessary user tracability metadata to the entities.
        /// </summary>
        private void UpdateDeletedDate()
        {
            foreach (EntityEntry<Entity> entity in ChangeTracker.Entries<Entity>())
            {
                if (entity.State == EntityState.Deleted)
                {
                    entity.State = EntityState.Modified;
                    entity.Entity.SetDeleted(DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Begin a transaction for the current instance of the <see cref="ApplicationDbContext"/>.
        /// </summary>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (CurrentTransaction != null)
            {
                return null;
            }

            CurrentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return CurrentTransaction;
        }

        /// <summary>
        /// Commit the <see cref="ApplicationDbContext"/> transaction to the database. The provided transaction must be the current
        /// transaction that the context instance is tracking.
        /// </summary>
        /// <param name="transaction">The current transaction.</param>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != CurrentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} cannot be committed because it is not current.");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }

        /// <summary>
        /// Rollback the current <see cref="ApplicationDbContext"/> transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                CurrentTransaction?.Rollback();
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }
    }
}
