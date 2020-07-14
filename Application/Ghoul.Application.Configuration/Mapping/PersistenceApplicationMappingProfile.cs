using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Persistence.Model;

namespace Ghoul.Application.Configuration.Mapping {
    public class PersistenceApplicationMappingProfile : Profile {
        public PersistenceApplicationMappingProfile()
        {
            CreateMap<BuildPersistenceModel, BaseBuildApplicationModel>()
                .ForMember(am => am.Status, opt => opt.MapFrom(pm => pm.Status));
            CreateMap<BuildPersistenceModel, BuildApplicationModel>()
                .ForMember(am => am.Status, opt => opt.MapFrom(pm => pm.Status))
                .ForMember(am => am.RepositoryURL, opt => opt.MapFrom(pm => pm.Repository != null ? pm.Repository.URL : ""))
                .ForMember(am => am.RepositoryTrigger, opt => opt.MapFrom(pm => pm.Repository != null ? pm.Repository.Trigger : null ));

            CreateMap<BuildStepPersistenceModel, BaseBuildStepApplicationModel>();
            CreateMap<BuildStepPersistenceModel, BuildStepApplicationModel>();
        }
    }
}