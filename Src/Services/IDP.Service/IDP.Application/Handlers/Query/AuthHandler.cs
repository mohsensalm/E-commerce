using Auth;
using IDP.Application.Querys.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Query
{
    public class AuthHandler : IRequestHandler<AuthQuery, bool>
    {
        private readonly IJWTHandller _jWTHandller;
        public AuthHandler(IJWTHandller jWTHandller)
        {
            _jWTHandller = jWTHandller;
        }
        public async Task<bool> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
           var token = _jWTHandller.Create(34);
            return  true;
        }
    }
}
