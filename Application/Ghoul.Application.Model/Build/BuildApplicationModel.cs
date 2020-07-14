using System;
using System.Collections.Generic;

namespace Ghoul.Application.Model.Build {
    public class BuildApplicationModel {
        // Questo è il modello intero
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        // Contiene anche repository URL, repository trigger
        public string RepositoryURL { get; set; } = "";
        public string RepositoryTrigger { get; set; } = null;
        // e gli step sono BuildStepApplicationModel, cioè modelli INTERI contenenti tutti i dati
        public IEnumerable<BuildStepApplicationModel> Steps { get; set; }

// questi due altri campi
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}