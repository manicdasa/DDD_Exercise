namespace GhostWriter.Infrastructure.Settings
{
    public class JWTConfigSetting
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public int ValidHours { get; set; }
    }
}
