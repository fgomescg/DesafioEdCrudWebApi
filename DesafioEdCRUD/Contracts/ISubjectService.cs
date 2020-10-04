using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISubjectService
    {
        ValueTask<PagedList<Subject>> GetSubjectsAsync(SubjectParameters subjectParameters);
        ValueTask<SubjectDto> GetSubjectByIdAsync(int subjectId);
        ValueTask<SubjectDto> CreateSubjectAsync(Subject subject);
        Task<bool> UpdateSubjectAsync(int id, SubjectPut subject);
        Task<bool> DeleteSubjectAsync(int id);        
    }
}
