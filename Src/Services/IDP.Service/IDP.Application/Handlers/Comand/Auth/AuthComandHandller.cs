using AutoMapper;
using DotNetCore.CAP;
using IDP.Application.Comands.Auth;
using IDP.Domain.Entites;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handlers.Comand.Auth
{
    public class AuthComandHandller : IRequestHandler<AuthComand, bool>
    {
        private readonly IOTPRepository _oTPRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        //private readonly IUserQueyRepository _userQueyRepository;
        private readonly IMapper _mapper;
        private readonly ICapPublisher _capPublisher ;


        public AuthComandHandller(IOTPRepository oTPRepository, ICapPublisher capPublisher)
        {
            _oTPRepository = oTPRepository;
            _capPublisher = capPublisher;
        }

        public async Task<bool> Handle(AuthComand request, CancellationToken cancellationToken)
        {
            try
            {
                var userobj = _mapper.Map<Domain.Entites.User>(request);
                //var user = _userQueyRepository.GetUserAsync(request.PhoneNumber);

                if (/*User = null*/  true)
                {




                }

            }
            catch (Exception)
            {

                throw;
            }
           await _oTPRepository.Insert(new Domain.DTO.OTP { UserID= 200 , OtpCode="gvrtd546", IsUse= false});
            return true; 
        }
    }
}
