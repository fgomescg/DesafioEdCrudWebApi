using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Repository
{
    public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
    {
        public SubjectRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
