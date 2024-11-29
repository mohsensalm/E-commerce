using IDP.Domain.DTO;
using IDP.Domain.IRepository.Command;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Command
{


    public class OTPRedisRepository : IOTPRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;

        public OTPRedisRepository(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
        }

        public async Task<bool> Delete(OTP entity)
        {
            await _distributedCache.RemoveAsync(entity.UserID.ToString());
            return true;
        }

        public Task<bool> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(OTP entity)
        {
            int time = Convert.ToInt32(_configuration.GetSection("Otp:OtpTime").Value);
            _distributedCache.SetString(entity.UserID.ToString(), JsonConvert.SerializeObject(entity), new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(time)).SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));
            return true;
        } 

        public Task<bool> Update(OTP entity)
        {
            throw new NotImplementedException();
        }
    }
}
