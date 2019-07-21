namespace Holistory.Common.Configuration
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiryMinutes { get; set; } = 120;
        public string SigningKey { get; set; }
    }
}
