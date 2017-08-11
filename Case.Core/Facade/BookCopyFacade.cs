using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Model;
using Case.Core.Repository.Interface;
using static Case.Core.Utils.BookUtils;

namespace Case.Core.Facade
{
    public class BookCopyFacade : IBookCopyFacade
    {
        private readonly IBookCopyRepository _repository;

        public BookCopyFacade(IBookCopyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddBookCopyAsync(BookCopy item)
        {
            VerifyBookCopy(item);

            return await _repository.InsertAsync(item);
        }

        public async Task<bool> UpdateBookCopyAsync(BookCopy item)
        {
            VerifyBookCopy(item);

            return await _repository.InsertOrUpdateAsync(item);
        }

        public async Task<bool> RemoveBookCopyAsync(BookCopy item)
        {
            VerifyBookCopy(item);

            return await _repository.DeleteAsync(item);
        }

        public async Task<List<BookCopy>> GetBookCopiesFromIsbn(string isbn)
        {
            if (!VerifyIsbn(isbn))
            {
                throw new ArgumentException("Isbn is invalid");
            }

            return await _repository.GetByIsbnAsync(isbn);
        }

        public Task<BookCopy> GetBookCopyById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveBookCopyAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id has to be greater or equal to 0");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> RemoveBookCopyAsync(string isbn)
        {
            if (!VerifyIsbn(isbn))
            {
                throw new ArgumentException("Isbn is invalid");
            }
            return await _repository.DeleteWithIsbnAsync(isbn);
        }

        // DRY
        private void VerifyBookCopy(BookCopy item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "BookCopy cannot be null");
            }

            if (!VerifyIsbn(item))
            {
                throw new ArgumentException("Isbn is invalid");
            }
        }
    }
}
