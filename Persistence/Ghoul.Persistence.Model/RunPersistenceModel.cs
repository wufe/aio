using System;
using System.Collections.Generic;
using Ghoul.Persistence.Model.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ghoul.Persistence.Model {
    public class RunPersistenceModel : IPrimaryKeyEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public IEnumerable<RunLogPersistenceModel> Logs { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string BuildID { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }

    public class RunLogPersistenceModel {
        public string LogType { get; set; }
        public string Content { get; set; }
    }
}