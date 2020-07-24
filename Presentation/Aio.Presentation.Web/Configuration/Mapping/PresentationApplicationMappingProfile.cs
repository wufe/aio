using AutoMapper;
using Aio.Application.Model;
using Aio.Application.Model.Build;
using Aio.Application.Model.Commands;
using Aio.Presentation.Model.Build;

namespace Aio.Presentation.Web.Configuration.Mapping {
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
            CreateMap<UpdateBuildInputModel, UpdateBuildCommand>();
            CreateMap<CreateStepInputModel, CreateStepCommand>();
            CreateMap<UpdateStepInputModel, UpdateStepCommand>();
            CreateMap<UpdateStepsOrderInputModel, UpdateStepsOrderCommand>();
        }
    }
}