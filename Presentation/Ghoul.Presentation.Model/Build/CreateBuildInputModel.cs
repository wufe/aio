using System;
using System.ComponentModel.DataAnnotations;
using Ghoul.Application.Model.Interface;

namespace Ghoul.Presentation.Model.Build
{
    public class CreateBuildInputModel : ICreateBuildModel
    {
        [Required]
        public string Name { get; set; }
    }
}
