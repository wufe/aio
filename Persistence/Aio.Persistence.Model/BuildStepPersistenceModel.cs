using System.Collections.Generic;
using Aio.Persistence.Model.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aio.Persistence.Model {
    public class BuildStepPersistenceModel {
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