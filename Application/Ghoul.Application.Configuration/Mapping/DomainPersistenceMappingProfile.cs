using AutoMapper;
using Ghoul.Domain.Entity;
using Ghoul.Persistence.Model;

namespace Ghoul.Application.Configuration.Mapping {
    public class DomainPersistenceMappingProfile : Profile {
        public DomainPersistenceMappingProfile()
        {
            CreateMap<BuildDomainEntity, BuildPersistenceModel>();
        }
    }
}