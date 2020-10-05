using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using Entities.DTO;

namespace DesafioEdCRUD.Controllers.V1
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _service;

        public AuthorController(IAuthorService authorService)
        {
            _service = authorService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAuthors([FromQuery] AuthorParameters authorParameters)
        {            
            var authors = await _service.GetAuthorsAsync(authorParameters);           

            var authorsResult = new
            {
                authors.TotalCount,
                authors.PageSize,
                authors.CurrentPage,
                authors.TotalPages,
                authors
            };              
            
            return Ok(authorsResult);            
        }


        [HttpGet("{Id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthorById(int Id)
        {            
            var authorDto = await _service.GetAuthorByIdAsync(Id);

            if (authorDto == null)
            {                
                return NotFound();
            }                           
            return Ok(authorDto);                       
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author authorFromBody)
        {
            var createdAuthor = await _service.CreateAuthorAsync(authorFromBody);

            return CreatedAtRoute("AuthorById", new { id = authorFromBody.AuthorId }, createdAuthor);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(int Id, [FromBody] AuthorPut authorPut)
        {            
            var authorEntity = await _service.UpdateAuthorAsync(Id, authorPut);

            if (!authorEntity)
            {                
                return NotFound();
            }
            
            return NoContent();           
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {           
            var authorEntity = await _service.GetAuthorByIdAsync(Id);

            if (authorEntity == null)
            {                
                return NotFound();
            }

            await _service.DeleteAuthorAsync(Id);            

            return NoContent();            
        }
    }
}
