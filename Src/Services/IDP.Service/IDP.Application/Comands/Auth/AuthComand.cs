using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Comands.Auth
{
    public class AuthComand : IRequest<bool>
    {
        public required string PhoneNumber { get; set;   }
    }
}
