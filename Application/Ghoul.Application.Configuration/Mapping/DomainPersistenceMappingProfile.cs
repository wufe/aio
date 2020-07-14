using AutoMapper;
using Ghoul.Domain.Entity;
using Ghoul.Domain.Entity.Build;
using Ghoul.Persistence.Model;

namespace Ghoul.Application.Configuration.Mapping {
    public class DomainPersistenceMappingProfile : Profile {
        public DomainPersistenceMappingProfile()
        {
            CreateMap<BuildDomainEntity, BuildPersistenceModel>()
                .ForMember(pm => pm.Status, opt => opt.MapFrom(de => de.Status.ToString()));
            CreateMap<BuildStepDomainEntity, BuildStepPersistenceModel>()
                .ForMember(pm => pm.Status, opt => opt.MapFrom(de => de.Status.ToString()));
            CreateMap<BuildRepositoryDomainEntity, BuildRepositoryPersistenceModel>();
        }
    }
}