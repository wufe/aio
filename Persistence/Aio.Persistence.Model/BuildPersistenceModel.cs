using System;
using System.Collections.Generic;
using Aio.Persistence.Model.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aio.Persistence.Model
{
    public class BuildPersistenceModel : IPrimaryKeyEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Order { get; private set; }
        public BuildRepositoryPersistenceModel Repository { get; set; }
        public IEnumerable<BuildStepPersistenceModel> Steps { get; set; } = new List<BuildStepPersistenceModel>();

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
