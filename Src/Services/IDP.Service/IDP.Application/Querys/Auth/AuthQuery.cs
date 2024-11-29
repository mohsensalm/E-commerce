using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Querys.Auth
{
    public record AuthQuery : IRequest<bool>
    {
        //public string? UserName { get; set; }
        //public string?   Password { get; set; }
        public required string PhoneNumber { get; set; } 

    }
}
