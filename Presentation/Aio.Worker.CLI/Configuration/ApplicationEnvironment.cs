namespace Aio.Worker.CLI.Configuration {
    public class ApplicationEnvironment {

        private bool _isDevelopment = false;
        private bool _isStaging = false;
        private bool _isProduction = false;

        private string _name = "Production";

        public ApplicationEnvironment(string environmentVariable)
        {
            if (string.IsNullOrEmpty(environmentVariable))
                environmentVariable = "Production";

            if (environmentVariable.ToLower().Trim() == "development") {
                _isDevelopment = true;
                _name = "Development";
            } else if (environmentVariable.ToLower().Trim() == "staging") {
                _isStaging = true;
                _name = "Staging";
            } else {
                _isProduction = true;
                _name = "Production";
            }
        }

        public bool IsDevelopment => _isDevelopment;
        public bool IsStaging => _isStaging;
        public bool IsProduction => _isProduction;

        public string Name => _name;
    }
}