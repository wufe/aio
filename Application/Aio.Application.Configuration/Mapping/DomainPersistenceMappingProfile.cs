using System;
using AutoMapper;
using Aio.Domain.Entity;
using Aio.Domain.Entity.Build;
using Aio.Persistence.Model;

namespace Aio.Application.Configuration.Mapping {
    public class DomainPersistenceMappingProfile : Profile {
        public DomainPersistenceMappingProfile()
        {
            CreateMap<BuildDomainEntity, BuildPersistenceModel>()
                .ForMember(pm => pm.Status, opt => opt.MapFrom(de => de.Status.ToString()));
            CreateMap<BuildPersistenceModel, BuildDomainEntity>()
                .ForMember(de => de.Status, opt => opt.ConvertUsing(new BuildStatusConverter(), pm => pm.Status));
            CreateMap<BuildStepDomainEntity, BuildStepPersistenceModel>()
                .ForMember(pm => pm.Status, opt => opt.MapFrom(de => de.Status.ToString()));
            CreateMap<BuildStepPersistenceModel, BuildStepDomainEntity>()
                .ForMember(de => de.Status, opt => opt.ConvertUsing(new BuildStepStatusConverter(), pm => pm.Status));
            CreateMap<BuildRepositoryDomainEntity, BuildRepositoryPersistenceModel>()
                .ReverseMap();
            CreateMap<RunDomainEntity, RunPersistenceModel>()
                .ReverseMap();
            CreateMap<RunLogDomainEntity, RunLogPersistenceModel>()
                .ForMember(pm => pm.LogType, opt => opt.MapFrom(de => de.LogType.ToString()))
                .ReverseMap();
        }

        private class BuildStatusConverter : IValueConverter<string, BuildStatus>
        {
            public BuildStatus Convert(string sourceMember, ResolutionContext context)
            {
                Enum.TryParse<BuildStatus>(sourceMember, out var status);
                return status;
            }
        }

        private class BuildStepStatusConverter : IValueConverter<string, BuildStepStatus>
        {
            public BuildStepStatus Convert(string sourceMember, ResolutionContext context)
            {
                Enum.TryParse<BuildStepStatus>(sourceMember, out var status);
                return status;
            }
        }
    }
}