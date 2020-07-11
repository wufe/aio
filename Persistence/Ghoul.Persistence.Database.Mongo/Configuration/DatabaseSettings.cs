using System.Collections.Generic;
using Ghoul.Persistence.Database.Mongo.Configuration.Interface;

namespace Ghoul.Persistence.Database.Mongo.Configuration {

    public class DatabaseSettings : IDatabaseSettings {
        public static string FIELD_NAME = "Database";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> CollectionNames { get; set; }
    }
}