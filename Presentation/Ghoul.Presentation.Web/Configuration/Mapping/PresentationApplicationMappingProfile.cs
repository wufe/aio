using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Commands;
using Ghoul.Presentation.Model.Build;

namespace Ghoul.Presentation.Web.Configuration.Mapping {
    public class PresentationApplicationMappingProfile : Profile {
        public PresentationApplicationMappingProfile()
        {
            CreateModelMappings();
            CreateCommandQueryMappings();
        }

        private void CreateModelMappings() {
            CreateMap<CreateBuildInputModel, BuildApplicationModel>();
        }

        private void CreateCommandQueryMappings() {
            CreateMap<CreateBuildInputModel, CreateBuildCommand>();
        }
    }
}