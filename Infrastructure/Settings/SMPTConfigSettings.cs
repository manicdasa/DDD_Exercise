namespace GhostWriter.Infrastructure.Settings
{
    public class SMPTConfigSettings
    {
        public bool Enabled { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string FromDisplay { get; set; }
    }
}
