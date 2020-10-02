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
            var authorEntity = await _repository.Author.GetAuthorById(Id);

            if (authorEntity == null)
            {
                _logger.LogError($"Author with id:{Id}, not found.");
                return NotFound();
            }
            else
            {
                var authorResult = _mapper.Map<AuthorDto>(authorEntity);
                _logger.LogInfo($"Returned author with id: {Id}");
                return Ok(authorResult);
            }           
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author authorFromBody)
        {  
            await _repository.Author.CreateAuthorAsync(authorFromBody);            

            var createdAuthorDto = _mapper.Map<AuthorDto>(authorFromBody);

            return CreatedAtRoute("AuthorById", new { id = authorFromBody.AuthorId }, createdAuthorDto);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(int Id, [FromBody] AuthorForCreateUpdateDto authorFromBody)
        {            
            var authorEntity = await _repository.Author.GetAuthorById(Id);

            if (authorEntity == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return NotFound();
            }

            var authorToUpdate = _mapper.Map(authorFromBody, authorEntity);

            await _repository.Author.UpdateAuthorAsync(authorToUpdate);            

            return NoContent();           
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {           
            var authorEntity = await _repository.Author.GetAuthorById(Id);

            if (authorEntity == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return NotFound();
            }

            await _repository.Author.DeleteAuthorAsync(authorEntity);            

            return NoContent();            
        }
    }
}
