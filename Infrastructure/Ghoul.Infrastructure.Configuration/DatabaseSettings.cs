using System.Collections.Generic;
using Ghould.Database.Mongo;

namespace Ghoul.Infrastructure.Configuration {

    public class DatabaseSettings : IDatabaseSettings {
        public static string FIELD_NAME = "Database";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> CollectionNames { get; set; }
    }
}