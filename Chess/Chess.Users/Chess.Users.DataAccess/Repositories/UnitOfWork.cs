﻿using Chess.Users.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool disposed;

        public UnitOfWork(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TRepositoryType> GetRepositoryAsync<TRepositoryType, TEntityType>()
            where TRepositoryType : IRepository
            where TEntityType: class, IBaseEntity
        {
            var repoType = typeof(TRepositoryType);

            if (!_repositories.ContainsKey(repoType))
            {
                var repo = (TRepositoryType)Activator.CreateInstance(typeof(TRepositoryType), new object[] { _dbContext.Set<TEntityType>() });
                _repositories.Add(repoType, repo);
            }

            return await Task.FromResult((TRepositoryType)_repositories[repoType]);
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;

            _dbContext.Dispose();
            disposed = true;
        }
        public async Task RollbackAsync()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

            await Task.CompletedTask;
        }
    }
}