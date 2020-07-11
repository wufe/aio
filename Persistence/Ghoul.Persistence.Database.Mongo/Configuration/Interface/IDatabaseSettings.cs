using System.Collections.Generic;

namespace Ghoul.Persistence.Database.Mongo.Configuration.Interface {
    public interface IDatabaseSettings {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        Dictionary<string, string> CollectionNames { get; set; }
    }
}