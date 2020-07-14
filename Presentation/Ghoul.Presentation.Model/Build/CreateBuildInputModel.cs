using System;
using System.ComponentModel.DataAnnotations;

namespace Ghoul.Presentation.Model.Build
{
    public class CreateBuildInputModel
    {
        [Required]
        public string Name { get; set; }
        public string RepositoryURL { get; set; }
    }
}
