namespace DataAccess.EntityMaps.Member
{
    public interface IRoleMap : IMap
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}