namespace Chess.ApiGateway.Api.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string[] Audiences { get; set; }
    }
}
