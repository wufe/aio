using System;
using System.Collections.Generic;
using System.Linq;
using Aio.Domain.Entity;
using Aio.Domain.Entity.Build;
using Aio.Domain.Entity.Build.Containers;
using Aio.Domain.Repository.Interface.Build;
using Aio.Domain.Service.Interface;

namespace Aio.Domain.Service {
    public class BuildDomainService : IBuildDomainService
    {
        private readonly IBuildRepository _buildRepository;

        public BuildDomainService(
            IBuildRepository buildRepository
        )
        {
            _buildRepository = buildRepository;
        }

        public BuildDomainEntity CreateBuild(string name) {
            name = name.Trim();
            var buildAlreadyExists = _buildRepository.BuildExistsByName(name);
            if (buildAlreadyExists)
                throw new ArgumentException($"Build named \"{name}\" already exists.");
            return BuildDomainEntity.CreateNew(name);
        }

        public BuildDomainEntity AppendNewStepToBuild(BuildDomainEntity build, string stepName) {
            if (build.Steps.Any(step => step.Name.ToLower() == stepName.ToLower()))
                throw new ArgumentException($"A step named \"{stepName}\" already exists.");

            build.AppendNewStep(stepName);

            return build;
        }

        public BuildRunContainer GetNextIdleRun() {
            return _buildRepository.GetNextIdleRun();
        }

        public RunDomainEntity CreateRun(string buildID)
        {
            if (!_buildRepository.BuildExistsByID(buildID))
                throw new ArgumentException($"Build with id \"{buildID}\" does not exist.");
            return RunDomainEntity.CreateNew(buildID);
        }

        public BuildRunContainer StartBuildRun(BuildDomainEntity build, RunDomainEntity run) {
            build.Start(run);
            run.Start(build);
            return new BuildRunContainer(build, run);
        }

        public BuildRunContainer StopBuildRun(BuildDomainEntity build, RunDomainEntity run) {
            build.Stop(run);
            run.Stop(build);
            return new BuildRunContainer(build, run);
        }

        public IEnumerable<BuildDomainEntity> UpdateBuildsOrder(int startIndex, int endIndex) {

            var buildDomainEntities = _buildRepository.GetAllBuilds().ToList();

            var swappingBuildDomainEntity = buildDomainEntities.ElementAt(startIndex);

            buildDomainEntities.RemoveAt(startIndex);
            buildDomainEntities.Insert(endIndex, swappingBuildDomainEntity);

            // Fix order
            for (int i = 0; i < buildDomainEntities.Count; i++)
                buildDomainEntities[i].SetOrder(i);

            return buildDomainEntities;
        }

    }
}