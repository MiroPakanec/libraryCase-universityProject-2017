using System.Collections.Generic;
using Case.Core.Entity;

namespace Case.Test.TestUtils
{
    public static class BookTestUtils
    { 
        public static Core.Entity.Book GenerateValidBook()
        {
            return new Core.Entity.Book()
            {
                Author = "J.K. Rowling",
                Binding = "2",
                Description = "Harry Potter",
                Edition = "1st",
                Isbn = "0439139600",
                Language = "English",
                Loanable = true,
                Subject = "Fiction",
                Title = "Harry Potter And The Goblet Of Fire"
            };
        }
        public static Core.Entity.Book GenerateValidBook_NotLoanable()
        {
            return new Core.Entity.Book()
            {
                Author = "J.K. Rowling",
                Binding = "2",
                Description = "Harry Potter",
                Edition = "1st",
                Isbn = "0439139600",
                Language = "English",
                Loanable = false,
                Subject = "Fiction",
                Title = "Harry Potter And The Goblet Of Fire"
            };
        }

        public static IEnumerable<Core.Entity.Book> GenerateValidBookList()
        {
            return new List<Core.Entity.Book>()
            {
                new Core.Entity.Book()
                {
                    Author = "J.K. Rowling",
                    Binding = "2",
                    Description = "Harry Potter",
                    Edition = "1st",
                    Isbn = "0439139600",
                    Language = "English",
                    Loanable = true,
                    Subject = "Fiction",
                    Title = "Harry Potter And The Goblet Of Fire"
                }
            };
        }

        public static Core.Entity.Book GenerateInvalidBook()
        {
            return new Core.Entity.Book()
            {
                Author = "J.K. Rowling",
                Binding = "2",
                Description = "Harry Potter",
                Edition = "1st",
                Isbn = "ISBN",
                Language = "English",
                Loanable = false,
                Subject = "Fiction",
                Title = "Harry Potter And The Goblet Of Fire"
            };
        }

        public static string GenerateValidDescription()
        {
            return "Harry Potter is midway through his training as a wizard and his coming of age";
        }

        public static string GenerateInvalidDescription()
        {
            return "";
        }
        
        public static string GenerateValidAuthor()
        {
            return "J.K. Rowling";
        }

        public static string GenerateInvalidAuthor()
        {
            return "";
        }
    }
}