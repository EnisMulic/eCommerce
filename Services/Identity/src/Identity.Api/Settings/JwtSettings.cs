namespace Identity.Api.Settings
{
    public class JwtSettings
    {
        public int TokenLifetimeMinutes { get; set; }
        public int PermanentTokenLifetimeDays { get; set; }
    }
}
