using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.IRepository.Command.Base
{
    public interface ICommandRepository<T> where T : class
    {
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteAll();

    }
}
