using System.ComponentModel.DataAnnotations;

namespace Ghoul.Presentation.Model.Build {
    public class UpdateBuildInputModel {
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(40)]
        public string Status { get; set; }

        [Url]
        public string RepositoryURL { get; set; }

        [MinLength(2)]
        [MaxLength(40)]
        public string RepositoryTrigger { get; set; }
    }
}