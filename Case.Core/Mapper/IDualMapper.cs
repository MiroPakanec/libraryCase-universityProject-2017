using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;

namespace Case.Core.Mapper
{
    public interface IDualMapper<T1,T2>
    {
        T2 Map(T1 item);
        T1 Unmap(T2 map);
    }
}
