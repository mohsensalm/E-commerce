 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.DTO
{ 
    public class OTP
    {
        public required Int64 UserID { get; set; }
        public required string OtpCode { get; set; }
        public bool IsUse { get; set; }
    }
}
