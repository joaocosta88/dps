namespace DPS.Service.Common
{
    public class 
        TokenSettings
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string SecretKey { get; init; }
        public int TokenExpireSeconds { get; init; }
        public int RefreshTokenExpireSeconds { get; init; }
        
        public bool ValidateIssuer { get; init; } 
        public bool ValidateAudience { get; init; } 
        public bool ValidateLifetime { get; init; }
        public bool ValidateIssuerSigningKey { get; init; } 
        
        public bool RequireExpirationTime { get; init; } 

    }
}
