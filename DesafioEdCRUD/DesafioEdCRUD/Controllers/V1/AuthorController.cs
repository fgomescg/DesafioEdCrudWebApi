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
    public class AuthorController : ControllerBase
    {
        private IAuthorService _service;

        public AuthorController(IAuthorService authorService)
        {
            _service = authorService;
        }

        [HttpGet(ApiRoutes.Authors.GetAll)]
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


        [HttpGet(ApiRoutes.Authors.Get, Name = "AuthorById")]
        public async Task<IActionResult> GetAuthorById(int authorId)
        {            
            var authorDto = await _service.GetAuthorByIdAsync(authorId);

            if (authorDto == null)
            {                
                return NotFound();
            }                           
            return Ok(authorDto);                       
        }

        [HttpPost(ApiRoutes.Authors.Create)]
        public async Task<IActionResult> CreateAuthor([FromBody] Author authorFromBody)
        {
            var createdAuthor = await _service.CreateAuthorAsync(authorFromBody);

            return CreatedAtRoute("AuthorById", new { authorId = authorFromBody.AuthorId }, createdAuthor);            
        }

        [HttpPut(ApiRoutes.Authors.Update)]
        public async Task<IActionResult> UpdateAuthor(int authorId, [FromBody] AuthorPut authorPut)
        {            
            var authorEntity = await _service.UpdateAuthorAsync(authorId, authorPut);

            if (!authorEntity)
            {                
                return NotFound();
            }
            
            return NoContent();           
        }

        [HttpDelete(ApiRoutes.Authors.Delete)]
        public async Task<IActionResult> DeleteAuthor(int authorId)
        {           
            var authorEntity = await _service.GetAuthorByIdAsync(authorId);

            if (authorEntity == null)
            {                
                return NotFound();
            }

            await _service.DeleteAuthorAsync(authorId);            

            return NoContent();            
        }
    }
}
