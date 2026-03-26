using System;

namespace TalkToAPI.V1.Models
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Expirtaion { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }
    }
}
