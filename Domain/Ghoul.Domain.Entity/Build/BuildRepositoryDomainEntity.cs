namespace Ghoul.Domain.Entity.Build {
    public class BuildRepositoryDomainEntity {
        public string URL { get; private set; }
        public string Trigger { get; private set; }

        public BuildRepositoryDomainEntity(string url, string trigger)
        {
            SetURL(url);
            SetTrigger(trigger);
        }

        public BuildRepositoryDomainEntity SetURL(string url) {
            URL = url;
            return this;
        }

        public BuildRepositoryDomainEntity SetTrigger(string trigger) {
            Trigger = trigger;
            return this;
        }
    }
}