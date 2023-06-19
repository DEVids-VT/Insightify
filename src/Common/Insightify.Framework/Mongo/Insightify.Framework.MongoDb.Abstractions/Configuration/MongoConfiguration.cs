using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.MongoDb.Abstractions.Configuration
{
    public class MongoConfiguration
    {
        public SoftDeleteConfiguration SoftDeleteConfiguration { get; private set; } = new();

        public string? ConnectionString { get; private set; }
        public string? Database { get; private set; }

        public BsonType EnumConvention { get; private set; } = BsonType.Int32;
        public bool IgnoreIfDefaultConvention { get; private set; } = true;

        public bool IgnoreIfNullConvention { get; private set; } = true;

        /// <summary>
        /// Sets the Mongo Connection String
        /// </summary>
        /// <param name="value">Connection String</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration WithConnectionString(string? value)
        {
            this.ConnectionString = value;
            return this;
        }

        /// <summary>
        /// Sets the Database Name
        /// </summary>
        /// <param name="value">Database Name</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration WithDatabaseName(string? value)
        {
            this.Database = value;
            return this;
        }

        /// <summary>
        /// Configures Soft Deletes
        /// </summary>
        /// <param name="value">Soft Delete Configuration</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration WithSoftDeletes(Action<SoftDeleteConfiguration> value)
        {
            var softDeleteConfig = new SoftDeleteConfiguration();
            value(softDeleteConfig);
            this.SoftDeleteConfiguration = softDeleteConfig;
            return this;
        }

        /// <summary>
        /// Sets how enums should be represented in the database
        /// </summary>
        /// <param name="value">Enum representation</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration RepresentEnumValuesAs(BsonType value = BsonType.Int32)
        {
            this.EnumConvention = value;
            return this;
        }

        /// <summary>
        /// Sets whether to ignore default values during serialization.
        /// </summary>
        /// <param name="ignoreIfDefault"></param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration WithIgnoreIfDefaultConvention(bool ignoreIfDefault = true)
        {
            IgnoreIfDefaultConvention = ignoreIfDefault;
            return this;
        }

        /// <summary>
        /// Sets whether to ignore null values during serialization.
        /// </summary>
        /// <param name="ignoreIfNull"></param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public MongoConfiguration WithIgnoreIfNullConvention(bool ignoreIfNull = true)
        {
            IgnoreIfNullConvention = ignoreIfNull;
            return this;
        }

    }
}
