using System;

namespace Ghoul.Application.Model {
    public class BuildApplicationModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}