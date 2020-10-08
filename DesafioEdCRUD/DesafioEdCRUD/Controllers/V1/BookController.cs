using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DesafioEdCRUD.Controllers.V1
{    
    [ApiController]    
    public class BookController : ControllerBase
    {       
        private IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;            
        }

        [HttpGet(ApiRoutes.Books.GetAll)]        
        public async ValueTask<IActionResult> GetBooksAsync([FromQuery] BookParameters bookParameters)
        {           
            var books = await _service.GetBooksAsync(bookParameters);            
                        
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


        [HttpGet(ApiRoutes.Books.Get, Name = "BookById")]
        public async ValueTask<IActionResult> GetBookById(int bookId)
        {           
            var bookDto = await _service.GetBookByIdAsync(bookId);

            if(bookDto == null)
            {                
                return NotFound();
            }            
            return Ok(bookDto);                      
        }       

        [HttpPost(ApiRoutes.Books.Create)]
        public async ValueTask<IActionResult> CreateBook([FromBody]Book book)
        {
            var createdBook = await _service.CreateBookAsync(book);                       

            return CreatedAtRoute("BookById", new { bookId = createdBook.Id }, createdBook);                   
        }

        [HttpPut(ApiRoutes.Books.Update)]
        public async ValueTask<IActionResult> UpdateBook(int bookId, [FromBody]BookPut bookPut)
        {  
            var isUpdated = await _service.UpdateBookAsync(bookId, bookPut);

            if (!isUpdated)
            {                
                return NotFound();
            }                 

            return NoContent();                        
        }

        [HttpDelete(ApiRoutes.Books.Delete)]
        public async ValueTask<IActionResult> DeleteBook(int bookId)
        {
            var isDeleted = await _service.DeleteBookAsync(bookId);
            
            if(!isDeleted)
            {
                return NotFound();
            }

            return NoContent();                      
        }              
    }
}
