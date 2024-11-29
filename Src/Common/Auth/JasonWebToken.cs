using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class JasonWebToken
    {
        public string? Token { get; set; }
        public long Expieres { get; set; }
        public long RefreshToken { get; set; }
    }
}
