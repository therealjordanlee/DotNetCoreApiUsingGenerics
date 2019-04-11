using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiUsingGenerics.Repositories
{
    public interface ITableRepository<TEntity>
    {
        Task<TEntity> FindRecord(string partitionKey, string rowKey);
    }
}
