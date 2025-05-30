using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UnitOfWork(storeDbContext dbContext) : IUnitOfWork
    {
        private readonly storeDbContext _dbContext = dbContext;
        private readonly Dictionary<string, object> _repositories = new();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(typeName, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }

            var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[typeName] = newRepository;
            return newRepository;
        }

        public async Task<int> saveChangesAsync() => await _dbContext.SaveChangesAsync();


    }
}