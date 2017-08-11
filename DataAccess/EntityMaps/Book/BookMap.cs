namespace DataAccess.EntityMaps.Book
{
    public class BookMap : IBookMap
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Language { get; set; }
        public string Binding { get; set; }
        public string Edition { get; set; }
        public string Author { get; set; }
        public bool Loanable { get; set; }
    }
}
