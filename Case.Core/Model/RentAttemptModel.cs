using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Model
{
    public class RentAttemptModel
    {
        public bool IsSuccesful { get; set; }
        public string ErrorMessage { get; set; }

        public static RentAttemptModel WhenUserIsInWrongRole()
        {
            return new RentAttemptModel()
            {
                IsSuccesful = false,
                ErrorMessage = "This role is not allowed to rent books"
            };
        }

        public static RentAttemptModel WhenOneOfTheBooksIsNotLoanable()
        {
            return new RentAttemptModel()
            {
                IsSuccesful = false,
                ErrorMessage = "This book is not loanable"
            };
        }

        public static RentAttemptModel WhenOneOfTheBooksDoesntHaveCopiesAvailable()
        {

            return new RentAttemptModel()
            {
                IsSuccesful = false,
                ErrorMessage = "No book copies are available at the moment"
            };
        }

        public static RentAttemptModel WhenSuccesful()
        {
            return new RentAttemptModel()
            {
                IsSuccesful = true,
                ErrorMessage = null
            };
        }
    }
}
