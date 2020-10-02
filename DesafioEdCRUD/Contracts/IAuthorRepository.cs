using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorRepository: IRepositoryBase<Author>
    {
        ValueTask<PagedList<Author>> GetAuthors(AuthorParameters authorParameters);        
        ValueTask<Author> GetAuthorById(int authorId);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
