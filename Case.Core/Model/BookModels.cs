using System;
using System.Collections.Generic;
using Case.Core.Entity;

namespace Case.Core.Model
{
    public class BookCatalogModel
    {
        public IEnumerable<BookCatalogItem> Books { get; set; }
        public bool IsBackAvailable { get; set; }
        public bool IsNextAvailable { get; set; }
        public int PageIndex { get; set; }

    }

    public class BookCatalogFilter
    {
        public BookCatalogFilter()
        {
            Author = "";
            Title = "";
            Subject = "";
            Isbn = "";
            Loanable = true;

            SetDateStart(DateTime.MinValue);
            SetDateEnd(DateTime.MaxValue);
        }

        public string Author { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
        public bool Loanable { get; set; }

        public DateTime PublishDateIntervalStart { get; private set; }
        public DateTime PublishDateIntervalEnd { get; private set; }

        public void SetDateStart(DateTime date)
        {
            if (date < DateTime.Now.AddYears(-25) || date > DateTime.Now.AddYears(15))
            {
                PublishDateIntervalStart = DateTime.Now.AddYears(-20);
            }
            else
            {
                PublishDateIntervalStart = date;
            }
        }

        public void SetDateEnd(DateTime date)
        {
            if (date < DateTime.Now.AddYears(-19) || date > DateTime.Now.AddYears(9))
            {
                PublishDateIntervalEnd = DateTime.Now.AddYears(10);
            }
            else
            {
                PublishDateIntervalEnd = date;
            }
        }
    }

    public class BookCatalogItem
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        // TODO Add more what's needed

        public static BookCatalogItem FromEntity(Book book)
        {
            return new BookCatalogItem() {Isbn = book.Isbn, Title = book.Title, Author = book.Author};
        }
    }

    
}