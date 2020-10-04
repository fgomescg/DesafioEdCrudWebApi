using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AuthorService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async ValueTask<PagedList<Author>> GetAuthorsAsync(AuthorParameters AuthorParameters)
        {
            var Authors = await _repository.Author.GetAuthorsAsync(AuthorParameters);

            var AuthorList = PagedList<Author>.ToPagedList(Authors.AsQueryable(),
                 AuthorParameters.PageNumber,
                 AuthorParameters.PageSize);

            return AuthorList;
        }

        public async ValueTask<AuthorDto> GetAuthorByIdAsync(int Id)
        {
            var authorEntity = await _repository.Author.GetAuthorByIdAsync(Id);

            if (authorEntity == null)
            {
                _logger.LogError($"Author with id:{Id}, not found.");
                return null;
            }

            var entityDto = _mapper.Map<AuthorDto>(authorEntity);
            _logger.LogInfo($"Returned book with id: {Id}");
            return entityDto;
        }

        public async ValueTask<AuthorDto> CreateAuthorAsync(Author Author)
        {
            await _repository.Author.CreateAuthorAsync(Author);

            var createdAuthor = _mapper.Map<AuthorDto>(Author);

            _logger.LogInfo($"Author created with id: {createdAuthor.AuthorId}");

            return createdAuthor;
        }

        public async Task<bool> UpdateAuthorAsync(int Id, AuthorPut AuthorPut)
        {
            var AuthorEntity = await _repository.Author.GetAuthorByIdAsync(Id);

            if (AuthorEntity == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return false;
            }

            _mapper.Map(AuthorPut, AuthorEntity);

            await _repository.Author.UpdateAuthorAsync(AuthorEntity);

            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int Id)
        {
            var Author = await _repository.Author.GetAuthorByIdAsync(Id);

            if (Author == null)
            {
                _logger.LogError($"Author with id: {Id}, not found in db.");
                return false;
            }

            await _repository.Author.DeleteAuthorAsync(Author);

            return true;
        }
    }
}
