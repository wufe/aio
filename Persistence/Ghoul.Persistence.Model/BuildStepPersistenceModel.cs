using System.Collections.Generic;
using Ghoul.Persistence.Model.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ghoul.Persistence.Model {
    public class BuildStepPersistenceModel : IPrimaryKeyEntity {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; } = "";
        public string Status { get; set; }
        public string CommandExecutable { get; set; } = "";
        public string CommandArguments { get; set; } = "";
        public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
        public string WorkingDirectory { get; set; } = "";
        public bool FireAndForget { get; set; } = false;
        public bool HaltOnError { get; set; } = true;
    }
}