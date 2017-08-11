using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Repository.Interface
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> FindById(int id);
        Task<IEnumerable<Role>> FindByName(string name);
    }
}
