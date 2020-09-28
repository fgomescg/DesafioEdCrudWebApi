using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorRepository: IRepositoryBase<Author>
    {
        Task<Author[]> GetAllAuthors(AuthorParameters authorParameters);
        Task<Author> GetAuthorById(int authorId);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
