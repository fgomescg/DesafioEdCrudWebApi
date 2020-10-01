using Entities;
using Entities.Models;
using Contracts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
    {
        private RepositoryContext repoContext;

        public SubjectRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            repoContext = repositoryContext;
        }

        public async Task<Subject[]> GetAllSubjects(SubjectParameters subjectParameters)
        {
            return await FindAll()
                    .OrderBy(su => su.Description)
                    .Skip((subjectParameters.PageNumber - 1) * subjectParameters.PageSize)
                    .Take(subjectParameters.PageSize)
                    .ToArrayAsync();
        }

        public async Task<Subject> GetSubjectById(int Id)
        {
            return await FindByCondition(sub => sub.SubjectId.Equals(Id)).FirstOrDefaultAsync();
        }

        public void CreateSubject(Subject subject)
        {
            Create(subject);
        }

        public void DeleteSubject(Subject subject)
        {
            Delete(subject);
        }

        public void UpdateSubject(Subject subject)
        {
            Update(subject);
        }

        public async Task SaveAsync()
        {
            await repoContext.SaveChangesAsync();
        }
    }
}
