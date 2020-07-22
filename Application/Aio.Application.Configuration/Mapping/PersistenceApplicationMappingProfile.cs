using System.Linq;
using AutoMapper;
using Aio.Application.Model;
using Aio.Application.Model.Build;
using Aio.Persistence.Model;

namespace Aio.Application.Configuration.Mapping {
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
            CreateMap<BuildStepPersistenceModel, BuildStepApplicationModel>()
                .ForMember(am => am.EnvironmentVariables,
                    opt => opt.MapFrom(pm =>
                        pm.EnvironmentVariables.Select(env => $"{env.Key}={env.Value}")));

            CreateMap<RunPersistenceModel, RunApplicationModel>();
            CreateMap<RunLogPersistenceModel, RunLogApplicationModel>();
        }
    }
}