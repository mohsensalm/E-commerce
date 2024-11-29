using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMessage.Events
{
    public class OTPEvent : BaseEvent
    {
        public string PhoneNumber { get; set; }
        public string OtpCode { get; set; }

    }
}
