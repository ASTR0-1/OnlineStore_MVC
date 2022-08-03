namespace OnlineStore_PL.Helpers
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Secret { get; set; }
        
        public string Lifetime { get; set; }
    }
}
