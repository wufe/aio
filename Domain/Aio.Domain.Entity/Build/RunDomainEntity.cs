using System;
using System.Collections.Generic;
using System.Linq;

namespace Aio.Domain.Entity.Build
{
    public class RunDomainEntity
    {
        public enum RunLogType
        {
            Stdout,
            Stderr
        }

        public string ID { get; private set; }
        public IEnumerable<RunLogDomainEntity> Logs { get; private set; }
        public string BuildID { get; private set; }
        public DateTime RequestedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }

        public static RunDomainEntity CreateNew(string buildID) {
            return new RunDomainEntity() {
                BuildID = buildID,
                Logs = new List<RunLogDomainEntity>(),
                RequestedAt = DateTime.UtcNow,
            };
        }

        public RunDomainEntity Start(BuildDomainEntity build) {
            if (StartedAt != null || EndedAt != null)
                throw new Exception($"Run already performed.");
            StartedAt = DateTime.UtcNow;
            return this;
        }

        public RunDomainEntity Stop(BuildDomainEntity build) {
            if (StartedAt == null)
                throw new Exception($"Run not started.");
            EndedAt = DateTime.UtcNow;
            return this;
        }

        public RunDomainEntity AddLog(string log, RunLogType logType)
        {
            var logs = Logs.ToList();
            logs.Add(new RunLogDomainEntity() { Content = log, LogType = logType });
            Logs = logs;
            return this;
        }
    }

    public class RunLogDomainEntity
    {
        public RunDomainEntity.RunLogType LogType { get; set; }
        public string Content { get; set; }
    }
}