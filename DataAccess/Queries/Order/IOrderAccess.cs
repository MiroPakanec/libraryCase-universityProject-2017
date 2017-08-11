using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Order;

namespace DataAccess.Queries.Order
{
    public interface IOrderAccess : IQueryable
    {
        Task<int> InsertWithIdReturn(IMap orderMap);
        Task<IOrderMap> GetById(int id);
    }
}
