using DotNetCoreApiUsingGenerics.Attributes;
using DotNetCoreApiUsingGenerics.Constants;
using Microsoft.Azure.Cosmos.Table;

namespace DotNetCoreApiUsingGenerics.Entities
{
    [TableName(TableNameConstants.MessageTable)]
    public class MessageEntity : TableEntity
    {
        public string Id => PartitionKey;
        public string SubId => RowKey;
        public string Message { get; set; }
    }
}
