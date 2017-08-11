using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository.Interface;
using DataAccess.Queries.OrderLine;

namespace Case.Core.Repository
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private IOrderLineAccess _access;
        private IMapper<OrderLine> _mapper;

        public OrderLineRepository(IOrderLineAccess access, IMapper<OrderLine> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public async Task<bool> InsertAsync(OrderLine item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var orderLineMap = _mapper.Map(item);

            if (orderLineMap == null)
            {
                throw new Exception("Couldn't map order line");
            }

            return await _access.InsertOneAsync(orderLineMap);
        }

        public Task<OrderLine> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(OrderLine item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(OrderLine item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrUpdateAsync(OrderLine item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
