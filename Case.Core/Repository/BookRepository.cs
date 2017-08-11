using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Repository.Interface;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Mapper.Book;
using Case.Core.Model;
using Case.Core.Utils;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.Queries.Book;

namespace Case.Core.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookAccess _access;
        private readonly IMapper<Book> _mapper;
        private readonly IMapper<BookCatalogFilter> _filterMapper;
        private readonly IDualMapper<Book, CatalogueItemMap> _bookCatalogueMapper;

        public BookRepository(IBookAccess access, IMapper<Book> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public BookRepository(IBookAccess access, IMapper<Book> mapper, IMapper<BookCatalogFilter> filterMapper,
            IDualMapper<Book, CatalogueItemMap> bookCatalogueItemMapper)
        {
            _access = access;
            _mapper = mapper;
            _filterMapper = filterMapper;
            _bookCatalogueMapper = bookCatalogueItemMapper;
        }

        public async Task<bool> InsertAsync(Book item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book cannot be null.");
            }

            var bookMap = _mapper.Map(item);

            if (bookMap == null)
            {
                throw new NullReferenceException("Book map cannot be null.");
            }

            return await _access.InsertOneAsync(bookMap as IBookMap);
        }

        public Task<Book> GetAsync(int id)
        {
            throw new Exception("Book cannot be searched by id.");
        }

        public async Task<bool> UpdateAsync(Book item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book cannot be null.");
            }

            var bookMap = _mapper.Map(item);

            if (bookMap == null)
            {
                throw new NullReferenceException("Book map cannot be null.");
            }

            return await _access.UpdateOneAsync(bookMap as IBookMap);
        }

        public async Task<bool> DeleteAsync(Book item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book cannot be null.");
            }

            if (item.Isbn == null || BookUtils.VerifyIsbn(item.Isbn) == false)
            {
                throw new Exception("Book isbn is not valid.");
            }

            var bookMap = _mapper.Map(item);

            if (bookMap == null)
            {
                throw new NullReferenceException("Book map cannot be null.");
            }

            return await _access.DeleteOneAsync(bookMap);
        }

        public async Task<bool> InsertOrUpdateAsync(Book item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book cannot be null.");
            }

            var bookMap = _mapper.Map(item);

            if (bookMap == null)
            {
                throw new NullReferenceException("Book map cannot be null.");
            }

            return await _access.InsertOrUpdateAsync(bookMap as IBookMap);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new Exception("Book cannot be deleted by id.");
        }

        [Obsolete("Use DeleteAsync")]
        public async Task<bool> DeleteWithIsbnAsync(string isbn)
        {
            if (isbn == null || BookUtils.VerifyIsbn(isbn) == false)
            {
                throw new Exception("Book isbn is not valid.");
            }

            return await _access.DeleteWithIdAsync(isbn);
        }

        public async Task<Book> GetByIsbnAsync(string isbn)
        {
            if (isbn == null || BookUtils.VerifyIsbn(isbn) == false)
            {
                throw new Exception("Book isbn is not valid.");
            }

            var bookMap = await _access.SelectWithIdAsync(isbn);

            if (bookMap == null)
            {
                return null;
            }

            return _mapper.Unmap(bookMap);
        }

        public async Task<Book> GetByDescription(string description)
        {
            var bookMaps = await _access.SelectWithDescription(description);
            //return bookMaps.Select(bookMap => _mapper.Unmap(bookMap)).ToList();

            throw new NotImplementedException();
        }

        public async Task<string> GetDescription(string isbn)
        {
            if (isbn == null || BookUtils.VerifyIsbn(isbn) == false)
            {
                throw new Exception("Book isbn is not valid.");
            }

            return await _access.SelectDescriptionWithId(isbn);
        }

        public Task<Book> GetByLoans(int loans)
        {
            if (loans < 0)
            {
                throw new Exception("Loan count is not valid.");
            }

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetPagedBooks(int pageId, int pageSize)
        {
            if (pageId < 0)
            {
                throw new ArgumentException("Page id cannot be null.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size has to be greater than 0.");
            }

            //todo: implement filter
            var filter = GetDefaultFilter;

            if (filter == null)
            {
                throw new NullReferenceException("Filter cannot be null.");
            }

            var filterMap = _filterMapper.Map(filter);

            if (filterMap == null)
            {
                throw new NullReferenceException("Filter map cannot be null.");
            }

            var catalogueItemMaps = await _access.SearchCatalogue(pageId, pageSize, filterMap);

            if (catalogueItemMaps == null)
            {
                throw new NullReferenceException("Catalogue item maps cannot be null.");
            }

            return catalogueItemMaps.Select(catalogueItemMap => _bookCatalogueMapper.Unmap(catalogueItemMap)).ToList();
        }

        public async Task<IEnumerable<Book>> GetPagedBooks(int pageId, int pageSize, BookCatalogFilter filter)
        {
            if (pageId < 0)
            {
                throw new ArgumentException("Page id cannot be null.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size has to be greater than 0.");
            }

            if (filter == null)
            {
                throw new ArgumentNullException($"Filter cannot be null.");
            }

            var filterMap = _filterMapper.Map(filter);

            if (filterMap == null)
            {
                throw new NullReferenceException("Filter map cannot be null.");
            }

            var catalogueItemMaps = await _access.SearchCatalogue(pageId, pageSize, filterMap);

            if (catalogueItemMaps == null)
            {
                throw new NullReferenceException("Catalogue item maps cannot be null.");
            }

            return catalogueItemMaps.Select(catalogueItemMap => _bookCatalogueMapper.Unmap(catalogueItemMap)).ToList();
        }

        public async Task<int> GetBookCount()
        {
           return await _access.Count();
        }

        public Task<Book> GetByAuthor(int loans, string author)
        {
            if (loans < 0 || author == null)
            {
                throw new Exception("Loan count and author cannot be invalid.");
            }
            
            throw new NotImplementedException();
        }

        private static BookCatalogFilter GetDefaultFilter => new BookCatalogFilter();

    }
}
