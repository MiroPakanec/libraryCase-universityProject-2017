using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Core.Repository.Interface;
using Case.Core.Facade.Interfaces;
using Case.Core.Entity;
using Case.Core.Exceptions;
using Case.Core.Model;
using Case.Core.Utils;
using static Case.Core.Utils.BookUtils;

namespace Case.Core.Facade
{
    public class BookFacade : IBookFacade
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;

        public BookFacade(IBookRepository bookRepository, IMemberRepository memberRepository, IBookCopyRepository bookCopyRepository, IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _bookCopyRepository = bookCopyRepository;
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
        }

        #region CRUD

        public async Task<bool> AddBookAsync(Book book)
        {
            VerifyBook(book);
            return await _bookRepository.InsertAsync(book);
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            VerifyBook(book);
            return await _bookRepository.UpdateAsync(book);
        }

        public async Task<bool> RemoveBookAsync(Book book)
        {
            VerifyBook(book);
            return await _bookRepository.DeleteAsync(book);
        }

        public async Task<Book> FindBookByIsbnAsync(string isbn)
        {
            if (!VerifyIsbn(isbn))
            {
               throw new ArgumentException("Isbn is invalid");
            }
            return await _bookRepository.GetByIsbnAsync(isbn);
        }

        public async Task<Book> FindBookByDescriptionAsync(string description)
        {
           if (string.IsNullOrEmpty(description))
           {
                throw new ArgumentException("Description is Invalid");
           }

           return await _bookRepository.GetByDescription(description);
        }

        public async Task<string> GetBookDescription(string isbn)
        {
            if (string.IsNullOrEmpty(isbn) || !VerifyIsbn(isbn))
            {
                throw new ArgumentException("Isbn Is Invalid");
            }
            return await _bookRepository.GetDescription(isbn);
        }

        #endregion

        public async Task<BookCatalogModel> GetPagedBookCatalogModel(int pageId, int pageSize)
        {
            if (pageId < 0)
            {
                throw new ArgumentException("PageId has to be positive or 0");
            }
            if(pageSize < 1)
            {
                throw new ArgumentException("PageSize has to be greater than 0");
            }

            var books = (await _bookRepository.GetPagedBooks(pageId, pageSize))
                .Select(BookCatalogItem.FromEntity)
                .ToList();

            var allBookCount = await GetBookCount();

            return new BookCatalogModel
            {
                IsBackAvailable = pageId > 0,
                IsNextAvailable = (pageId+1) * pageSize <allBookCount,
                PageIndex = pageId + 1,
                Books = books
            };
        }

        
        public async Task<int> GetBookCount()
        {
        var count = await _bookRepository.GetBookCount();

        if (count < 0)
        {
            throw new ArgumentException("Book count cannot be smaller than 0.");
        }

        return count;
    }
        // TODO handle cases when it fails to insert data into db
        public async Task<RentAttemptModel> RentBooks(string ssn, IEnumerable<string> isbns)
        {
            if (!MemberUtils.ValidateSsn(ssn))
            {
                throw new ArgumentException("Invalid isbn");
            }

            if (isbns == null || !isbns.Any())
            {
                throw new ArgumentException("Isbn list cannot be empty when renting books");
            }

            var member = await _memberRepository.FindBySsn(ssn);
            var role = await _memberRepository.GetRoleName(member);
            if (!Roles.CanRent(role))
            {
                return RentAttemptModel.WhenUserIsInWrongRole();
            }

            var order = new Order()
            {
                MemberId = ssn,
                Created = DateTime.UtcNow
            };

            var orderId = await _orderRepository.InsertAsyncWithIdReturn(order);

            foreach (var isbn in isbns)
            {
                var book = await _bookRepository.GetByIsbnAsync(isbn);
                if (!book.Loanable)
                {
                    return RentAttemptModel.WhenOneOfTheBooksIsNotLoanable();
                }

                var availableCopies = (await _bookCopyRepository.GetByIsbnAsync(isbn))
                    .Where(x => x.Available).ToList();
                if (availableCopies.Count == 0)
                {
                    return RentAttemptModel.WhenOneOfTheBooksDoesntHaveCopiesAvailable();
                }

                var chosenCopy = availableCopies[0];
                chosenCopy.Available = false;

                var orderLine = BookRentUtils.CreateOrderLineModel(orderId, chosenCopy.Id);
                await _orderLineRepository.InsertAsync(orderLine);

                await _bookCopyRepository.UpdateAsync(chosenCopy);
            }
            return RentAttemptModel.WhenSuccesful();
        }

        public async Task<BookCatalogItem> GetBookCatalogItemModelByIsbn(string isbn)
        {
            if (!VerifyIsbn(isbn))
            {
                throw new ArgumentException("Isbn is invalid");
            }

            var book = await FindBookByIsbnAsync(isbn);
            return BookToCatalogItemModel(book);
        }

        public async Task<IEnumerable<BookCatalogItem>> GetBookCatalogItemModelsByIsbns(string[] isbns)
        {
            if (isbns == null || isbns.Length == 0)
            {
                throw new ArgumentException("Isbn list cannot be null or empty");
            }

            var result = new List<BookCatalogItem>();
            foreach (var isbn in isbns)
            {
                result.Add(await GetBookCatalogItemModelByIsbn(isbn));
            }
            return result;
        }

        public async Task<Book> FindBookByLoansAsync(int loans)
        {
            if (loans < 0)
            {
                throw new ArgumentException("Number of loans is invalid.");
            }

            return await _bookRepository.GetByLoans(loans);
        }

        public async Task<Book> FindPopularBookByAuthorAsync(int loans, string author)
        {
            if (loans < 0 || string.IsNullOrEmpty(author))
            {
                throw new ArgumentException("Loaned count and author cannot be invalid.");
            }

            return await _bookRepository.GetByAuthor(loans, author);
        }
    }
}