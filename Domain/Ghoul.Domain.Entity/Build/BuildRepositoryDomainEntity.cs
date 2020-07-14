namespace Ghoul.Domain.Entity.Build {
    public class BuildRepositoryDomainEntity {
        public string URL { get; private set; }
        public string Trigger { get; private set; }

        public BuildRepositoryDomainEntity(string url, string trigger)
        {
            URL = url;
            Trigger = trigger;
        }
    }
}