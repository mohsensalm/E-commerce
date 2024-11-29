using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class JWTOptions
    {
        public  string? SecreteKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string? Issuer { get; set; }
    }
}
