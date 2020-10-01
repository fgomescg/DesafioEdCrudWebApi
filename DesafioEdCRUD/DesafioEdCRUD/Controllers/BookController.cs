using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using AutoMapper;
using Entities.DTO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entities.Models.Exceptions;

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
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBookss: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        

        [HttpGet("{Id}", Name ="BookById")]
        public async ValueTask<IActionResult> GetBookById(int Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"GetBookById: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }       

        [HttpPost]
        public IActionResult CreateBook([FromBody]BookForCreateDto book)
        {
            try
            {
                if (book == null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var bookEntity = _mapper.Map<Book>(book);

                _repository.Book.CreateBook(bookEntity);
                _repository.Book.Save();

                var createdBook = _mapper.Map<BookDto>(bookEntity);

                return CreatedAtRoute("BookById", new { id = createdBook.Id }, createdBook);
            }           
            catch (Exception ex)
            {                
                _logger.LogError($"CreateBook: {ex}");
                return StatusCode(500, "Internal server error");
            }            
        }

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> UpdateBook(int Id, [FromBody]BookForUpdateDto bookObj)
        {
            try
            {
                if (bookObj == null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var bookEntity = await _repository.Book.GetBookById(Id);

                if (bookEntity == null)
                {
                    _logger.LogError($"Book with id: {Id}, not found in db.");
                    return NotFound();
                }

                _mapper.Map(bookObj, bookEntity);

                _repository.Book.UpdateBook(bookEntity);
                _repository.Book.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateBook: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteBook(int Id)
        {
            try
            {
                var book = await _repository.Book.GetBookById(Id);

                if (book == null)
                {
                    _logger.LogError($"Book with id: {Id}, not found in db.");
                    return NotFound();
                }

                _repository.Book.DeleteBook(book);
                _repository.Book.Save();

                return NoContent();
            }            
            catch (Exception ex)
            {
                _logger.LogError($"DeleteBook action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }            
        }

        [HttpGet("report")]
        public async ValueTask<IActionResult> GetBookAuthorReports()
        {
            try
            {
                var bookReport = await _repository.Book.GetBookAuthorReports();

                _logger.LogInfo($"Returned bookReport from database.");
                
                return Ok(bookReport);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBookAuthorReports: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
