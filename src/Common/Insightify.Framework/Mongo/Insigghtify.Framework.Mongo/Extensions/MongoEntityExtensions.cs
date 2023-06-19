using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.MongoDb.Abstractions.Attributes;

namespace Insightify.Framework.Mongo.Extensions
{
    public static class MongoEntityExtensions
    {
        /// <summary>
        /// Gets the Collection Name for an Entity
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <returns>Collection Name</returns>
        public static string GetCollectionName<T>() where T : class, IMongoEntity
        {
            var collectionNameAttribute = (CollectionNameAttribute)typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault();
            return collectionNameAttribute != null ? collectionNameAttribute.Name.ToLower() : typeof(T).Name.ToLower();
        }
    }
}
