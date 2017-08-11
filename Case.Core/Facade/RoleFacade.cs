using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using Microsoft.AspNet.Identity;

namespace Case.Core.Facade
{
    public class RoleFacade : IRoleFacade, IRoleStore<Role>
    {
        private readonly IRoleRepository _repository;

        public RoleFacade(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await _repository.InsertAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await _repository.UpdateAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }   
            await _repository.DeleteAsync(role);
        }

        public async Task<Role> FindByIdAsync(string roleId)
        {
            int id = Int32.Parse(roleId);

            if (id < 0)
            {
                throw new ArgumentNullException(nameof(roleId));
            }
            return await _repository.FindById(id);
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return (await _repository.FindByName(roleName)).FirstOrDefault();
        }

        public void Dispose()
        {
            //TODO maybe make repository implement disposable and dispose it here
        }
    }
}
