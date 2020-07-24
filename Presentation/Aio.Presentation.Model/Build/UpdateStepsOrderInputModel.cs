using System.ComponentModel.DataAnnotations;

namespace Aio.Presentation.Model.Build {
    public class UpdateStepsOrderInputModel {

        [Required]
        public int StartIndex { get; set; }
        [Required]
        public int EndIndex { get; set; }
    }
}