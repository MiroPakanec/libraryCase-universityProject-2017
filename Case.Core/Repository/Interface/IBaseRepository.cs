using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Repository.Interface
{
    public interface IBaseRepository <T>
    {
        Task<bool> InsertAsync(T item);
        Task<T> GetAsync(int id);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<bool> InsertOrUpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
    }
}
