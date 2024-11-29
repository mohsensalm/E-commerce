using IDP.Application.Comands.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Comand.User
{
    public class UserHandler : IRequestHandler<UserComand, bool>
    {
      
        public async Task<bool> Handle(UserComand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
