using System;
using System.ComponentModel.DataAnnotations;

namespace Ghoul.Presentation.Model.Build
{
    public class CreateBuildInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }
        public string RepositoryURL { get; set; }
    }
}
