using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Repository
{
    public class AuthorRepository: RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
