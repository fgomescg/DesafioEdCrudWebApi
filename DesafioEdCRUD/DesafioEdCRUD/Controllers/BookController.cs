using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using AutoMapper;
using Entities.DTO;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public BookController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllBooks([FromQuery] BookParameters bookParameters)
        {           
            PagedList<Book> books = await _repository.Book.GetAllBooks(bookParameters);

            _logger.LogInfo($"Returned {books.TotalCount} books from database.");                

            var booksResult = new
            {
                books.TotalCount,
                books.PageSize,
                books.CurrentPage,
                books.TotalPages,
                books
            };
           
            return Ok(booksResult);            
        }
        

        [HttpGet("{Id}", Name ="BookById")]
        public async ValueTask<IActionResult> GetBookById(int Id)
        {           
            var book = await _repository.Book.GetBookById(Id);

            if(book == null)
            {
                _logger.LogError($"Book with id:{Id}, not found.");
                return NotFound();
            }
            else
            {
                var bookResult = _mapper.Map<BookDto>(book);
                _logger.LogInfo($"Returned book with id: {Id}");
                return Ok(bookResult);
            }          
        }       

        [HttpPost]
        public async ValueTask<IActionResult> CreateBook([FromBody]Book book)
        { 
            await _repository.Book.CreateBookAsync(book);            

            var createdBook = _mapper.Map<BookDto>(book);

            return CreatedAtRoute("BookById", new { id = createdBook.Id }, createdBook);                   
        }

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> UpdateBook(int Id, [FromBody]BookForUpdateDto bookObj)
        {  
            var bookEntity = await _repository.Book.GetBookById(Id);

            if (bookEntity == null)
            {
                _logger.LogError($"Book with id: {Id}, not found in db.");
                return NotFound();
            }

            _mapper.Map(bookObj, bookEntity);

            await _repository.Book.UpdateBookAsync(bookEntity);            

            return NoContent();                        
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteBook(int Id)
        {           
            var book = await _repository.Book.GetBookById(Id);

            if (book == null)
            {
                _logger.LogError($"Book with id: {Id}, not found in db.");
                return NotFound();
            }

            await _repository.Book.DeleteBookAsync(book);            

            return NoContent();                      
        }

        [HttpGet("report")]
        public async ValueTask<IActionResult> GetBookAuthorReports()
        {            
            var bookReport = await _repository.Book.GetBookAuthorReports();

            _logger.LogInfo($"Returned bookReport from database.");
                
            return Ok(bookReport);           
        }
    }
}
