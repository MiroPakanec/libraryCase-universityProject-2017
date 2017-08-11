namespace DataAccess.EntityMaps.Book
{
    public interface IBookMap : IMap
    {
        string ISBN { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Subject { get; set; }
        string Language { get; set; }
        string Binding { get; set; }
        string Edition { get; set; }
        string Author { get; set; }
        bool Loanable { get; set; }
    }
}
