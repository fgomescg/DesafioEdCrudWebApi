using Entities.DTO;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorService
    {
        ValueTask<PagedList<Author>> GetAuthorsAsync(AuthorParameters authorParameters);
        ValueTask<AuthorDto> GetAuthorByIdAsync(int subjectId);
        ValueTask<AuthorDto> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(int id, AuthorPut author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
