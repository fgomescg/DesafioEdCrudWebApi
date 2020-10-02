using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorRepository: IRepositoryBase<Author>
    {
        ValueTask<PagedList<Author>> GetAuthors(AuthorParameters authorParameters);        
        ValueTask<Author> GetAuthorById(int authorId);
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
    }
}
