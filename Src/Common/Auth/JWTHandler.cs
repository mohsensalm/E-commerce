using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Permissions;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace Auth
{
    public class JWTHandler : IJWTHandller

    {

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandeler = new JwtSecurityTokenHandler();
        private readonly JWTOptions _options;
        private readonly SecurityKey _issuersecuritykey;
        private readonly SigningCredentials _signingCredintial;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenvaldstor;

        public JWTHandler(IOptions<JWTOptions> jwtOption)
        {
            _options = jwtOption.Value;
            _issuersecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: _options.SecreteKey));
            _signingCredintial = new SigningCredentials(_issuersecuritykey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredintial);
            _tokenvaldstor = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuersecuritykey,


            };

        }
         

        public JasonWebToken Create(Int64 userId)
        {

            var utcnow = DateTime.UtcNow;
            var expiers = utcnow.AddMinutes(_options.ExpiryMinutes);
            var cnturybegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long )(new TimeSpan(expiers.Ticks - cnturybegin.Ticks).TotalMilliseconds);
            var now = (long )(new TimeSpan(expiers.Ticks - cnturybegin.Ticks).TotalMilliseconds);

            var peylod = new JwtPayload
            {
                {"sub",userId },
                { "iss",_options.Issuer},
                {"int",now },
                {"exp",exp },
                {"unique_code",userId },

            };
            var jwt = new JwtSecurityToken(_jwtHeader, peylod);
            var token = _jwtSecurityTokenHandeler.WriteToken(jwt);
            return new JasonWebToken
            {
                Token = token,
                Expieres = exp
            }; 

        }
    }
}
