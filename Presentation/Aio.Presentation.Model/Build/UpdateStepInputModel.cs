using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aio.Presentation.Model.Build {
    public class UpdateStepInputModel {

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
        [MaxLength(200)]
        public string CommandExecutable { get; set; }
        [MaxLength(200)]
        public string CommandArguments { get; set; }
        public IEnumerable<string> EnvironmentVariables { get; set; } =  new List<string>();
        [MaxLength(200)]
        public string WorkingDirectory { get; set; }
        public bool FireAndForget { get; set; }
        public bool HaltOnError { get; set; }
        
    }
}