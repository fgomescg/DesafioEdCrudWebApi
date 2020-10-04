using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorRepository: IRepositoryBase<Author>
    {
        ValueTask<List<Author>> GetAuthorsAsync(AuthorParameters authorParameters);        
        ValueTask<Author> GetAuthorByIdAsync(int authorId);
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
    }
}
