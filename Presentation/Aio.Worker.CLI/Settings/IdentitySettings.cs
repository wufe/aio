namespace Aio.Worker.CLI.Settings {
    public class IdentitySettings {
        public static string SECTION_NAME = "Identity";
        
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}