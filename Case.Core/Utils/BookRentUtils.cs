using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Utils
{
    public class BookRentUtils
    {
        public static IEnumerable<OrderLine> CreateOrderLines(int orderId, IEnumerable<int> bookCopyIds)
        {
            var orderLines = new List<OrderLine>();
            foreach (var copyId in bookCopyIds)
            {
                orderLines.Add(CreateOrderLineModel(orderId, copyId));
            }
            return orderLines;
        }

        public static OrderLine CreateOrderLineModel(int orderId, int bookCopyId)
        {
            return new OrderLine()
            {
                BookCopyId = bookCopyId,
                Extended = false,
                OrderId = orderId,
                Returned = false
            };
        }
    }
}
