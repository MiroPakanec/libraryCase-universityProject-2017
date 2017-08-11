using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Moq;
using static Case.Test.TestUtils.BookCopyTestUtils;

namespace Case.Test.Facade.Extensions
{
    public static class BookCopyRepositoryMockExtensions
    {
        public static Mock<IBookCopyRepository> WithValidInsert(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<BookCopy>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidDelete(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.DeleteAsync(It.IsAny<BookCopy>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidUpdate(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.UpdateAsync(It.IsAny<BookCopy>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidGet(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(GenerateValidBookCopy());
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidGetByIsbn(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(new List<BookCopy>() {GenerateValidBookCopy()});
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidInserOrUpdate(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.InsertOrUpdateAsync(It.IsAny<BookCopy>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IBookCopyRepository> WithEmptyFindByIsbn(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(new List<BookCopy>());
            return repository;
        }

        public static Mock<IBookCopyRepository> WithValidFindByIsbn(this Mock<IBookCopyRepository> repository)
        {
            repository.Setup(x => x.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(new List<BookCopy>() {GenerateValidBookCopy()});
            return repository;
        }
    }
}
