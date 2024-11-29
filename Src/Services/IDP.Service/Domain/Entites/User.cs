  using IDP.Domain.Entites.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entites
{
    public class User : BaseEntity.BaseEntity
    {
        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Salt { get; set; }

        
    } 
} 
