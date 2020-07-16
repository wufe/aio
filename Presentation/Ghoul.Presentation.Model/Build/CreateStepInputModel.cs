using System.ComponentModel.DataAnnotations;

namespace Ghoul.Presentation.Model.Build {
    public class CreateStepInputModel {

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }
        
    }
}