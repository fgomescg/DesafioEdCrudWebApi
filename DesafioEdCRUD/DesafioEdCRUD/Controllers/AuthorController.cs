using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using AutoMapper;
using Entities.DTO;
using System.Collections.Generic;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AuthorController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAuthors([FromQuery] AuthorParameters authorParameters)
        {            
            var authors = await _repository.Author.GetAuthors(authorParameters);

            _logger.LogInfo($"Returned {authors.TotalCount} authors from database.");

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
            var author = await _repository.Author.GetAuthorById(Id);

            if (author == null)
            {
                _logger.LogError($"Author with id:{Id}, not found.");
                return NotFound();
            }
            else
            {
                var authorResult = _mapper.Map<AuthorDto>(author);
                _logger.LogInfo($"Returned author with id: {Id}");
                return Ok(authorResult);
            }           
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreateUpdateDto author)
        {  
            var authorEntity = _mapper.Map<Author>(author);

            _repository.Author.CreateAuthor(authorEntity);
            _repository.Author.Save();

            var createdAuthor = _mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("AuthorById", new { id = createdAuthor.AuthorId }, createdAuthor);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(int Id, [FromBody]AuthorForCreateUpdateDto AuthorObj)
        {
            
            var AuthorEntity = await _repository.Author.GetAuthorById(Id);

            if (AuthorEntity == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return NotFound();
            }

            _mapper.Map(AuthorObj, AuthorEntity);

            _repository.Author.UpdateAuthor(AuthorEntity);
            _repository.Author.Save();

            return NoContent();           
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {           
            var Author = await _repository.Author.GetAuthorById(Id);

            if (Author == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return NotFound();
            }

            _repository.Author.DeleteAuthor(Author);
            _repository.Author.Save();

            return NoContent();            
        }
    }
}
