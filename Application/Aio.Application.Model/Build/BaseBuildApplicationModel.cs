using System.Collections.Generic;

namespace Aio.Application.Model.Build {
    // Base model used for lists
    public class BaseBuildApplicationModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public IEnumerable<BaseBuildStepApplicationModel> Steps { get; set; }
    }
}