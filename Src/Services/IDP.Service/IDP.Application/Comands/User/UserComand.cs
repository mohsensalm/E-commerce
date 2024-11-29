using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Comands.User
{
    public class UserComand : IRequest<bool>
    {
        [Required(ErrorMessage ="need field")]
        [MinLength(4)]

        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
     }
}
