using System;

namespace Case.Core.Model.Extensions
{
    public static class BookCatalogFilterExtensions
    {
        public static BookCatalogFilter WithIsbn(this BookCatalogFilter filter, string isbn)
        {
            filter.Isbn = isbn;
            return filter;
        }

        public static BookCatalogFilter WithAuthor(this BookCatalogFilter filter, string author)
        {
            filter.Author = author;
            return filter;
        }

        public static BookCatalogFilter WithSubject(this BookCatalogFilter filter, string subject)
        {
            filter.Subject = subject;
            return filter;
        }

        public static BookCatalogFilter WithTitle(this BookCatalogFilter filter, string title)
        {
            filter.Title = title;
            return filter;
        }

        public static BookCatalogFilter WithLoanability(this BookCatalogFilter filter, bool canLoan)
        {
            filter.Loanable = canLoan;
            return filter;
        }

        public static BookCatalogFilter WithStartDate(this BookCatalogFilter filter, DateTime date)
        {
            filter.SetDateStart(date);
            return filter;
        }

        public static BookCatalogFilter WithEndDate(this BookCatalogFilter filter, DateTime date)
        {
            filter.SetDateEnd(date);
            return filter;
        }
    }
}
