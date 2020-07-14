using System;
using System.ComponentModel.DataAnnotations;

namespace Ghoul.Presentation.Model.Build
{
    // classe usata lato PRESENTAZIONALE con validazione aspnet core
    public class CreateBuildInputModel
    {
        // Name e repositoryurl

        [Required]
        public string Name { get; set; }
        public string RepositoryURL { get; set; }
    }
}
