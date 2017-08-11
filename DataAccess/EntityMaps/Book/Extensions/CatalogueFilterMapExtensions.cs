using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Book.Extensions
{
    public static class CatalogueFilterMapExtensions
    {
        public static CatalogueFilterMap WithIsbn(this CatalogueFilterMap filter, string isbn)
        {
            filter.Isbn = isbn;
            return filter;
        }

        public static CatalogueFilterMap WithAuthor(this CatalogueFilterMap filter, string author)
        {
            filter.Author = author;
            return filter;
        }

        public static CatalogueFilterMap WithSubject(this CatalogueFilterMap filter, string subject)
        {
            filter.Subject = subject;
            return filter;
        }

        public static CatalogueFilterMap WithTitle(this CatalogueFilterMap filter, string title)
        {
            filter.Title = title;
            return filter;
        }

        public static CatalogueFilterMap WithLoanability(this CatalogueFilterMap filter, bool canLoan)
        {
            filter.CanLoan = canLoan;
            return filter;
        }

        public static CatalogueFilterMap WithStartDate(this CatalogueFilterMap filter, DateTime date)
        {
            filter.SetDateStart(date);
            return filter;
        }

        public static CatalogueFilterMap WithEndDate(this CatalogueFilterMap filter, DateTime date)
        {
            filter.SetDateEnd(date);
            return filter;
        }
    }
}
