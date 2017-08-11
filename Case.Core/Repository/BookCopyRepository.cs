using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using DataAccess.EntityMaps.BookCopy;
using DataAccess.Queries.BookCopy;

namespace Case.Core.Repository
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly IBookCopyAccess _access;
        private readonly IMapper<BookCopy> _mapper;

        public BookCopyRepository(IBookCopyAccess access, IMapper<BookCopy> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public Task<bool> InsertAsync(BookCopy item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book copy cannot be null.");
            }

            var bookCopyMap = _mapper.Map(item);

            if (bookCopyMap == null)
            {
                throw new NullReferenceException("Book copy map cannot be null.");
            }

            return _access.InsertOneAsync(bookCopyMap as IBookCopyMap);
        }

        public Task<BookCopy> GetAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid book copy id");
            }

            return GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(BookCopy item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book copy cannot be null.");
            }

            var bookCopyMap = _mapper.Map(item);

            if (bookCopyMap == null)
            {
                throw new NullReferenceException("Book copy map cannot be null.");
            }

            return _access.UpdateOneAsync(bookCopyMap as IBookCopyMap);
        }

        public Task<bool> DeleteAsync(BookCopy item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book copy cannot be null.");
            }

            return DeleteAsync(item.Id);
        }

        public Task<bool> InsertOrUpdateAsync(BookCopy item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Book copy cannot be null.");
            }

            var bookCopyMap = _mapper.Map(item);

            if (bookCopyMap == null)
            {
                throw new NullReferenceException("Book copy map cannot be null.");
            }

            return _access.InsertOrUpdateAsync(bookCopyMap as IBookCopyMap);
        }

        public Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid book copy id");
            }

            return _access.DeleteWithIdAsync(id);
        }

        public async Task<List<BookCopy>> GetByIsbnAsync(string isbn)
        {
            if (isbn == null || BookUtils.VerifyIsbn(isbn) == false)
            {
                throw new ArgumentException("Invalid isbn");
            }
            var bookCopyMaps = await _access.GetByIsbnAsync(isbn);

            return bookCopyMaps.Select(bookCopyMap => _mapper.Unmap(bookCopyMap)).ToList();
        }

        public async Task<BookCopy> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid book copy id");
            }

            var bookCopyMap = await _access.GetByIdAsync(id);

            if (bookCopyMap == null)
            {
                throw new NullReferenceException("Book copy map cannot be null.");
            }

            return _mapper.Unmap(bookCopyMap);
        }

        public async Task<IEnumerable<BookCopy>> GetAllAsync()
        {
            var bookCopyMaps = await _access.SelectAllAsync();

            if (bookCopyMaps == null)
            {
                throw new NullReferenceException("Book copy maps cannot be null.");
            }

            return bookCopyMaps.Select(bookCopyMap => _mapper.Unmap(bookCopyMap)).ToList();
        }

        public Task<bool> DeleteWithIsbnAsync(string isbn)
        {
            if (isbn == null || BookUtils.VerifyIsbn(isbn) == false)
            {
                throw new ArgumentException("Isbn is not valid.");
            }

            return _access.DeleteWithIsbnAsync(isbn);
        }
    }
}
