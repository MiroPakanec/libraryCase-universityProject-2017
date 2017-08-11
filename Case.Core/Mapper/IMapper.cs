using DataAccess.EntityMaps;

namespace Case.Core.Mapper
{
    public interface IMapper<T>
    {
        IMap Map(T item);
        T Unmap(IMap map);
    }
}
    