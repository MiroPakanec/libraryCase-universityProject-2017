namespace Case.Core.Entity
{
    public class Book
    {
        public virtual string Isbn { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Language { get; set; }
        public virtual string Binding { get; set; }
        public virtual string Edition { get; set; }
        public virtual string Author { get; set; }
        public virtual bool Loanable { get; set; }
    }
}