using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using AutoMapper;
using Entities.DTO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Entities.Models.Books.Exceptions;
using DesafioEdCRUD.Services;
using Entities.Models.Books;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetBooks([FromQuery] BookParameters bookParameters)
        {
            try
            {
                var books = await _bookService.GetBooks(bookParameters);                                       
           
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        

        [HttpGet("{Id}", Name ="BookById")]
        public async ValueTask<IActionResult> GetBookById(int Id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(Id);

                /*if(book == null)
                {                    
                    return NotFound();
                }
                else
                {
                    var bookResult = _mapper.Map<BookDto>(book);
                    
                    
                }*/
                return Ok(book);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "Internal server error");
            }
        }       

        [HttpPost]
        public async ValueTask<IActionResult> CreateBook([FromBody]Book book)
        {
            try
            {
                 var storageBook = await _bookService.CreateBookAsync(book);
                
                /* if (book == null)
                 {
                     return BadRequest("Book object is null");
                 }

                 if (!ModelState.IsValid)
                 {
                     return BadRequest("Invalid model object");
                 }

                 var bookEntity = _mapper.Map<Book>(book);

                 _repository.Book.CreateBook(bookEntity);
                 _repository.Book.Save();

                 var createdBook = _mapper.Map<BookDto>(bookEntity);*/
                

                return CreatedAtRoute("BookById", new { id = storageBook.Id }, storageBook);


            }
            catch (BookValidationException bookValidationException)
                when (bookValidationException.InnerException is AlreadyExistsBookException)
            {                
                return Conflict(bookValidationException.InnerException.Message);
            }
            catch (BookValidationException bookValidationException)
            {             
                return BadRequest(bookValidationException.InnerException.Message);
            }
            catch (BookRepositoryException bookRepositoryException)
            {                
                return Problem(bookRepositoryException.Message);
            }                
        }

        /*[HttpPut("{Id}")]
        public async ValueTask<IActionResult> UpdateBook(int Id, [FromBody]BookForUpdateDto bookObj)
        {
            try
            {
                if (bookObj == null)
                {
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var bookEntity = await _bookService.GetBookById(Id);

                if (bookEntity == null)
                {
                    return NotFound();
                }

                *//*_mapper.Map(bookObj, bookEntity);

                _repository.Book.UpdateBook(bookEntity);
                _repository.Book.Save();*//*

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
        }*/
    }
}
