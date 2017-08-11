using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Book
{
    public class CatalogueFilterMap : ICatalogueFilterMap
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
        public bool CanLoan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void SetDateStart(DateTime date)
        {
            if (date < DateTime.Now.AddYears(-25) || date > DateTime.Now.AddYears(15))
            {
                StartDate = DateTime.Now.AddYears(-20);
            }
            else
            {
                StartDate = date;
            }
        }

        public void SetDateEnd(DateTime date)
        {
            if (date < DateTime.Now.AddYears(-19) || date > DateTime.Now.AddYears(9))
            {
                EndDate = DateTime.Now.AddYears(10);
            }
            else
            {
                EndDate = date;
            }
        }
    }
}
