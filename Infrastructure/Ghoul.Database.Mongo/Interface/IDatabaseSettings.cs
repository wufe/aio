using System.Collections.Generic;

namespace Ghould.Database.Mongo {
    public interface IDatabaseSettings {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        Dictionary<string, string> CollectionNames { get; set; }
    }
}