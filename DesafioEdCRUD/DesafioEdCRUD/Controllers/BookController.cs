using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using Entities.DTO;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {       
        private IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;            
        }

        [HttpGet]
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
        

        [HttpGet("{Id}", Name ="BookById")]
        public async ValueTask<IActionResult> GetBookById(int Id)
        {           
            var bookDto = await _service.GetBookByIdAsync(Id);

            if(bookDto == null)
            {                
                return NotFound();
            }            
            return Ok(bookDto);                      
        }       

        [HttpPost]
        public async ValueTask<IActionResult> CreateBook([FromBody]Book book)
        {
            var createdBook = await _service.CreateBookAsync(book);                       

            return CreatedAtRoute("BookById", new { id = createdBook.Id }, createdBook);                   
        }

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> UpdateBook(int Id, [FromBody]BookPut bookPut)
        {  
            var isUpdated = await _service.UpdateBookAsync(Id, bookPut);

            if (!isUpdated)
            {                
                return NotFound();
            }                 

            return NoContent();                        
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteBook(int Id)
        {
            var isDeleted = await _service.DeleteBookAsync(Id);
            
            if(!isDeleted)
            {
                return NotFound();
            }

            return NoContent();                      
        }

        [HttpGet("report")]
        public async ValueTask<IActionResult> GetBookAuthorReports()
        {            
            var bookReport = await _service.GetBookAuthorReports();            
                
            return Ok(bookReport);           
        }
    }
}
