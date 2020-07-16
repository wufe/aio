using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ghoul.Domain.Entity.Build {

    public enum BuildStatus {
        Idle,
        Running
    }

    public class BuildDomainEntity {
        public string ID { get; set; }
        public string Name { get; private set; }
        public BuildStatus Status { get; private set; }
        public BuildRepositoryDomainEntity Repository { get; private set; }
        public IEnumerable<BuildStepDomainEntity> Steps { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private BuildDomainEntity() { }

        public static BuildDomainEntity CreateNew(string name) {
            return new BuildDomainEntity {
                Name = name,
                Status = BuildStatus.Idle,
                Repository = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Steps = new List<BuildStepDomainEntity>()
            };
        }

        public BuildDomainEntity SetRepository(string url, string trigger = null) {
            if (Repository == null)
                Repository = new BuildRepositoryDomainEntity(url, trigger);
            return this;
        }

        public BuildDomainEntity SetStatus(BuildStatus status) {
            Status = status;
            return this;
        }

        public BuildDomainEntity AppendNewStep(string name) {
            var stepsList = Steps.ToList();
            stepsList.Add(BuildStepDomainEntity.CreateNew(name));
            Steps = stepsList;
            return this;
        }

        public BuildStepDomainEntity WithStepAt(int index) {
            return Steps.ElementAt(index);
        }
    }
}