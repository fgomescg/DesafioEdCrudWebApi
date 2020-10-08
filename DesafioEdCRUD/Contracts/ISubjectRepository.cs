using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubjectRepository : IRepositoryBase<Subject>
    {
        ValueTask<List<Subject>> GetSubjectsAsync(SubjectParameters subjectParameters);       
        ValueTask<Subject> GetSubjectByIdAsync(int subjectId);
        Task CreateSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(Subject subject);
    }
}
