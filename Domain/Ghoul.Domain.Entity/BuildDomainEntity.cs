namespace Ghoul.Domain.Entity {
    public class BuildDomainEntity {

        public string Name { get; private set; }

        private BuildDomainEntity() { }

        public static BuildDomainEntity Build(string name) {
            return new BuildDomainEntity {
                Name = name
            };
        }
    }
}