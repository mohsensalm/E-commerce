using IDP.Domain.Entites;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly ShopQueryDBContext  _shopDBContext;

        public UserQueryRepository(ShopQueryDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        public async Task<User> GetUserAsync(string phoneNumber)
        {
            var user = await _shopDBContext.Tbl_User.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            return user;

        }
    }
}
