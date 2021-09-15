using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool authenticated, string createdAt, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            CreatedAt = createdAt;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool Authenticated { get; set; }
        public string CreatedAt { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
