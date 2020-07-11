using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Persistence.Model;

namespace Ghoul.Application.Configuration.Mapping {
    public class PersistenceApplicationMappingProfile : Profile {
        public PersistenceApplicationMappingProfile()
        {
            CreateMap<BuildPersistenceModel, BuildApplicationModel>();
        }
    }
}