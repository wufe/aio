using System.Collections.Generic;

namespace Ghould.Persistence.Database.Mongo.Configuration.Interface {
    public interface IDatabaseSettings {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        Dictionary<string, string> CollectionNames { get; set; }
    }
}