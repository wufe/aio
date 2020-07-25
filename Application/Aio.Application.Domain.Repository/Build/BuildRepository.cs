using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Aio.Domain.Entity.Build;
using Aio.Domain.Entity.Build.Containers;
using Aio.Domain.Repository.Interface.Build;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using System;

namespace Aio.Application.Domain.Repository.Build {
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

        public IEnumerable<BuildDomainEntity> GetAllBuilds() {
            var buildPersistenceModels = _buildRepository
                    .FindAll()
                    .OrderBy(b => b.Order)
                    .ToList();
            var buildDomainEntities = _mapper.Map<BuildDomainEntity[]>(buildPersistenceModels);
            return buildDomainEntities;
        }

        public int GetHighestBuildOrder() {
            return _buildRepository
                .FindAll()
                .OrderByDescending(b => b.Order)
                .FirstOrDefault()?.Order ?? 0;
        }
    }
}