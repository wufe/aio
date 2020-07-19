using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ghoul.Domain.Entity.Build.Containers;

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
        public static object StepOutcome { get; set; }

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

        public BuildDomainEntity SetName(string name) {
            Name = name;
            return this;
        }

        public BuildDomainEntity SetRepository(string url, string trigger = null) {
            if (Repository == null)
                Repository = new BuildRepositoryDomainEntity(url, trigger);
            else {
                Repository.SetURL(url);
                Repository.SetTrigger(trigger);
            }
            return this;
        }

        public BuildDomainEntity RunStep(int stepIndex)
        {
            var step = Steps.ElementAt(stepIndex);
            step.Run();
            return this;
        }

        public BuildDomainEntity RemoveStep(int stepIndex) {
            Steps = Steps.Where((s, i) => i != stepIndex);
            return this;
        }

        public BuildDomainEntity AppendNewStep(string name) {
            var stepsList = Steps.ToList();
            stepsList.Add(BuildStepDomainEntity.CreateNew(name));
            Steps = stepsList;
            return this;
        }

        public BuildDomainEntity StopStep(int stepIndex, BuildStepDomainEntity.StepOutcome outcome)
        {
            var step = Steps.ElementAt(stepIndex);
            step.Stop(outcome);
            return this;
        }

        public BuildDomainEntity Start(RunDomainEntity run) {
            if (Status != BuildStatus.Idle)
                throw new Exception($"Build is busy.");
            Status = BuildStatus.Running;
            foreach (var step in Steps)
                step.WaitForRun();
            return this;
        }

        public BuildDomainEntity Stop(RunDomainEntity run) {
            if (Status != BuildStatus.Running)
                throw new Exception($"Build is not running.");
            Status = BuildStatus.Idle;
            foreach (var step in Steps)
                step.BuildStopped();
            return this;
        }

        public BuildStepContainer GetNextWaitingStep() {
            if (Status != BuildStatus.Running)
                throw new Exception($"Build is not running.");

            BuildStepDomainEntity step = null;
            var stepIndex = -1;
            for (var i = 0; i < Steps.Count(); i++) {
                if (step == null && Steps.ElementAt(i).Status == BuildStepStatus.Waiting) {
                    step = Steps.ElementAt(i);
                    stepIndex = i;
                }
            }
            if (step == null)
                return null;
            return new BuildStepContainer(step, stepIndex);
        }

        public BuildStepDomainEntity WithStepAt(int index)
        {
            return Steps.ElementAt(index);
        }
    }
}