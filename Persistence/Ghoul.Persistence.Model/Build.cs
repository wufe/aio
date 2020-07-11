using System;
using Ghoul.Persistence.Model.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ghoul.Persistence.Model
{
    public class Build : IPrimaryKeyEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
