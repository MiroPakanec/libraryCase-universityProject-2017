using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Case.Test.TestUtils.BookTestUtils;

namespace Case.Test.Facade.Extensions
{
    public static class BookRepositoryMockExtensions
    {
        public static Mock<IBookRepository> WithValidInsert(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<Book>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookRepository> WithValidDelete(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.DeleteAsync(It.IsAny<Book>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookRepository> WithValidUpdate(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookRepository> WithValidGet(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(GenerateValidBook());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetByIsbn(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(GenerateValidBook());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetByIsbn_BookNotLoanable(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(GenerateValidBook_NotLoanable);
            return repository;
        }

        public static Mock<IBookRepository> WithValidInserOrUpdate(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.InsertOrUpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetByDescription(this Mock<IBookRepository> repository)
        {

            repository.Setup(x => x.GetByDescription(It.IsAny<string>())).ReturnsAsync(GenerateValidBook());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetByLoans(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetByLoans(It.IsAny<int>())).ReturnsAsync(GenerateValidBook());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetByAuthorLoans(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(GenerateValidBook());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetBookCount(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetBookCount()).ReturnsAsync(1);
            return repository;
        }

        public static Mock<IBookRepository> WithInvalidGetBookCount(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetBookCount()).ReturnsAsync(-1);
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetDescription(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetDescription(It.IsAny<string>())).ReturnsAsync(It.IsAny<string>());
            return repository;
        }

        public static Mock<IBookRepository> WithValidGetPagedBooks(this Mock<IBookRepository> repository)
        {
            repository.Setup(x => x.GetPagedBooks(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int id, int size) => Enumerable.Repeat(GenerateValidBook(), size).ToList());
            return repository;
        }
    }
}