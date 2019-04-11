using System;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCoreApiUsingGenerics.Attributes;
using Microsoft.Azure.Cosmos.Table;

namespace DotNetCoreApiUsingGenerics.Repositories
{
    public class TableRepository<TEntity> : ITableRepository<TEntity> where TEntity: TableEntity
    {
        private readonly CloudTableClient _tableClient;

        public TableRepository(CloudTableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task<TEntity> FindRecord(string partitionKey, string rowKey)
        {
            var test = typeof(TEntity);
            var tableNameAttribute = typeof(TEntity).GetCustomAttribute<TableNameAttribute>();
            if (tableNameAttribute == null)
            {
                throw new Exception("Missing table attribute");
            }

            var tableName = tableNameAttribute.Name;
            var table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            TableOperation retrieveOperation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(retrieveOperation);

            return result.Result as TEntity;
        }
    }
}
