using System;

namespace Ghoul.Domain.Entity {
    public class BuildDomainEntity {

        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private BuildDomainEntity() { }

        public static BuildDomainEntity CreateNew(string name) {
            return new BuildDomainEntity {
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}