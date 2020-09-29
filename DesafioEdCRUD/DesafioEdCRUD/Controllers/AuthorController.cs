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
        public async Task<IActionResult> GetAllAuthors([FromQuery] AuthorParameters authorParameters)
        {
            try
            {
                var authors = await _repository.Author.GetAllAuthors(authorParameters);

                _logger.LogInfo($"Returned all authors from database.");

                var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(authors);
                return Ok(authorsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllAuthors: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{Id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"GetAuthorById: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreateUpdateDto author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Author object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var authorEntity = _mapper.Map<Author>(author);

                _repository.Author.CreateAuthor(authorEntity);
                _repository.Author.Save();

                var createdAuthor = _mapper.Map<AuthorDto>(authorEntity);

                return CreatedAtRoute("AuthorById", new { id = createdAuthor.AuthorId }, createdAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateAuthor: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(int Id, [FromBody]AuthorForCreateUpdateDto AuthorObj)
        {
            try
            {
                if (AuthorObj == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Author object sent from client.");
                    return BadRequest("Invalid model object");
                }
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
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAuthor: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"DeleteAuthor action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
