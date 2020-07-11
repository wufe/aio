using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ghoul.Persistence.Model
{
    public class Webhook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string Method { get; set; }
        public string Endpoint { get; set; }
        public string FullPath { get; set; }
    }
}
