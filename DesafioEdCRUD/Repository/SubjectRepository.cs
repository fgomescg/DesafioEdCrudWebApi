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
        public SubjectRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }       
        public async ValueTask<PagedList<Subject>> GetSubjects(SubjectParameters subjectParameters)
        {
            var subjects = FindAll();

            SearchByDescription(ref subjects, subjectParameters.description);

            await subjects.OrderBy(su => su.Description)
                            .ToListAsync();

            return PagedList<Subject>.ToPagedList(subjects,
                   subjectParameters.PageNumber,
                   subjectParameters.PageSize);
        }
        private void SearchByDescription(ref IQueryable<Subject> subjects, string subjectDescription)
        {
            if (!subjects.Any() || string.IsNullOrWhiteSpace(subjectDescription))
                return;
            subjects = subjects.Where(o => o.Description.ToLower().Contains(subjectDescription.Trim().ToLower()));
        }
        public async ValueTask<Subject> GetSubjectById(int Id)
        {
            return await FindByCondition(sub => sub.SubjectId.Equals(Id)).FirstOrDefaultAsync();
        }
        public async Task CreateSubjectAsync(Subject subject)
        {
            await CreateAsync(subject);
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            await UpdateAsync(subject);
        }
        public async Task DeleteSubjectAsync(Subject subject)
        {
            await DeleteAsync(subject);
        }
    }
}
