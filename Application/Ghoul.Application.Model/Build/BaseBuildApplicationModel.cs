using System.Collections.Generic;

namespace Ghoul.Application.Model.Build {
    // Base model used for lists
    public class BaseBuildApplicationModel {


        // Ha poche proprietà: ID, Name, Status e un array di BASEStep ( non Step con tutti i dati, ma anche loro son modelli più piccoli )
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public IEnumerable<BaseBuildStepApplicationModel> Steps { get; set; }
    }
}