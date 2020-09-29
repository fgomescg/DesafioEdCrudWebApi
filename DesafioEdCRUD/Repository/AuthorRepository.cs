using Entities;
using Entities.Models;
using System;
using Contracts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AuthorRepository: RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Author[]> GetAllAuthors(AuthorParameters authorParameters)
        {
            return await FindAll()
                    .OrderBy(bk => bk.Name)
                    .Skip((authorParameters.PageNumber - 1) * authorParameters.PageSize)
                    .Take(authorParameters.PageSize)
                    .ToArrayAsync();
        }

        public async Task<Author> GetAuthorById(int Id)
        {
            return await FindByCondition(author => author.AuthorID.Equals(Id)).FirstOrDefaultAsync();
        }

        public void CreateAuthor(Author author)
        {
            Create(author);
        }
        public void UpdateAuthor(Author author)
        {
            Update(author);
        }
        public void DeleteAuthor(Author author)
        {
            Delete(author);
        }       
    }
}
