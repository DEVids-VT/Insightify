namespace Insightify.Framework.MongoDb.Abstractions
{
    using Insightify.Framework.MongoDb.Abstractions.Interfaces;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Base Class for a Mongo Entity
    /// </summary>
    public abstract class MongoEntity : IMongoEntity
    {   
        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id", Order = 1)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <inheritdoc/>
        [BsonElement("_version", Order = 3)]
        public int RowVersion { get; set; } = 1;

        /// <inheritdoc/>
        [BsonElement("_createdDateTime", Order = 4)]
        [BsonRepresentation(BsonType.String)]
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc/>
        [BsonElement("_updatedDateTime", Order = 5)]
        [BsonRepresentation(BsonType.String)]
        public DateTime UpdatedDateTime { get; set; }

        /// <inheritdoc/>
        [BsonElement("_deletedDateTime", Order = 6)]
        public DateTime? DeletedDateTime { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public bool IsDeleted => this.DeletedDateTime != null;
    }
}
