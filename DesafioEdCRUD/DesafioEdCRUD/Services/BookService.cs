using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;

namespace DesafioEdCRUD.Services
{
    public class BookService : IBookService
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public BookService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async ValueTask<PagedList<Book>> GetBooksAsync(BookParameters bookParameters)
        {
            var books = await _repository.Book.GetBooksAsync(bookParameters);
                        
            var bookList = PagedList<Book>.ToPagedList(books.AsQueryable(),
                 bookParameters.PageNumber,
                 bookParameters.PageSize);          

            return bookList;            
        }

        public async ValueTask<BookDto> GetBookByIdAsync(int Id)
        {
            var book = await _repository.Book.GetBookByIdAsync(Id);

            if (book == null)
            {
                _logger.LogError($"Book with id:{Id}, not found.");
                return null;
            }
            
            var bookResult = _mapper.Map<BookDto>(book);
            _logger.LogInfo($"Returned book with id: {Id}");
            return bookResult;
        }

        public async Task<BookDto> CreateBookAsync(Book book)
        {
            await _repository.Book.CreateBookAsync(book);

            var createdBook = _mapper.Map<BookDto>(book);

            _logger.LogInfo($"Book created with id: {createdBook.Id}");

            return createdBook;
        }

        public async Task<bool> UpdateBookAsync(int Id, BookForUpdateDto book)
        {
            var bookEntity = await _repository.Book.GetBookByIdAsync(Id);

            if (bookEntity == null)
            {
                _logger.LogError($"Book with id: {Id}, not found in db.");
                return false;
            }

            _mapper.Map(book, bookEntity);

            await _repository.Book.UpdateBookAsync(bookEntity);

            return true;            
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            var book = await _repository.Book.GetBookByIdAsync(Id);

            if (book == null)
            {
                _logger.LogError($"Book with id: {Id}, not found in db.");
                return false;
            }

            await _repository.Book.DeleteBookAsync(book);

            return true;
        }
        
        public async ValueTask<BookAuthorReport[]> GetBookAuthorReports()
        {
            return await _repository.Book.GetBookAuthorReports();
        }       

    }
}
