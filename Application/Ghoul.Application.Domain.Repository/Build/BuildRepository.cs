using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ghoul.Domain.Entity.Build;
using Ghoul.Domain.Entity.Build.Containers;
using Ghoul.Domain.Repository.Interface.Build;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;

namespace Ghoul.Application.Domain.Repository.Build {
    public class BuildRepository : IBuildRepository
    {
        private readonly IMapper _mapper;
        private readonly IReadRepository<BuildPersistenceModel> _buildRepository;
        private readonly IReadRepository<RunPersistenceModel> _runRepository;

        public BuildRepository(
            IMapper mapper,
            IReadRepository<BuildPersistenceModel> buildRepository,
            IReadRepository<RunPersistenceModel> runRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
            _runRepository = runRepository;
        }

        public bool BuildExistsByName(string name)
        {
            return _buildRepository
                .FindAll(x => x.Name.ToLower() == name.ToLower())
                .Any();
        }

        public bool BuildExistsByID(string buildID)
        {
            return _buildRepository.Find(buildID) != null;
        }

        public BuildRunContainer GetNextIdleRun() {

            var availableBuilds = _buildRepository
                .FindAll(x => x.Status == "Idle")
                .Select(x => x.ID)
                .ToList();

            var runPersistenceModel = _runRepository
                .FindAll(x => availableBuilds.Contains(x.BuildID) && x.EndedAt == null)
                .OrderBy(x => x.RequestedAt)
                .FirstOrDefault();

            if (runPersistenceModel == null)
                return null;

            var buildPersistenceModel = _buildRepository
                .Find(runPersistenceModel.BuildID);

            if (buildPersistenceModel == null) // dangling situation?
                return null;

            var runDomainEntity = _mapper.Map<RunDomainEntity>(runPersistenceModel);
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            return new BuildRunContainer(buildDomainEntity, runDomainEntity);
        }
    }
}