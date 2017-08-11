using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository.Interface;
using DataAccess.Queries.Order;

namespace Case.Core.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderAccess _access;
        private IMapper<Order> _mapper;

        public OrderRepository(IOrderAccess access, IMapper<Order> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public async Task<bool> InsertAsync(Order item)
        {
            if (item == null || item.Id < 0)
            {
                throw new ArgumentException("Item cannot be invalid or null");
            }

            return (await InsertAsyncWithIdReturn(item)) > 0;
        }

        public async Task<int> InsertAsyncWithIdReturn(Order item)
        {
            if (item == null || item.Id < 0)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var orderMap = _mapper.Map(item);

            if (orderMap == null)
            {
                throw new Exception("Couldn't map order");
            }

            return await _access.InsertWithIdReturn(orderMap);
        }

        public Task<Order> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrUpdateAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
