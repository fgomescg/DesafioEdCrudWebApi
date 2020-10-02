using Entities;
using Entities.Models;
using System;
using Contracts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository
{
    public class AuthorRepository: RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async ValueTask<PagedList<Author>> GetAuthors(AuthorParameters authorParameters)
        {
            var authors = FindAll();
            
            SearchByName(ref authors, authorParameters.Name);

            await authors.OrderBy(p => p.Name).ToArrayAsync();

            return PagedList<Author>.ToPagedList(authors,
                     authorParameters.PageNumber,
                     authorParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Author> authors, string authorName)
        {
            if (!authors.Any() || string.IsNullOrWhiteSpace(authorName))
                return;
            authors = authors.Where(o => o.Name.ToLower().Contains(authorName.Trim().ToLower()));
        }

        public async ValueTask<Author> GetAuthorById(int Id)
        {
            return await FindByCondition(author => author.AuthorId.Equals(Id)).FirstOrDefaultAsync();
        }

        public async Task CreateAuthorAsync(Author author)
        {
            await CreateAsync(author);
        }
        public async Task UpdateAuthorAsync(Author author)
        {
            await UpdateAsync(author);
        }
        public async Task DeleteAuthorAsync(Author author)
        {
            await DeleteAsync(author);
        }       
    }
}
