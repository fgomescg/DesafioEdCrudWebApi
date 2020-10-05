using System.Threading.Tasks;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEdCRUD.Controllers.V1
{
    [ApiController]
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects([FromQuery] SubjectParameters subjectParameters)
        {    
            var subjects = await _service.GetSubjectsAsync(subjectParameters);            

            var subjectsResult = new
            {
                subjects.TotalCount,
                subjects.PageSize,
                subjects.CurrentPage,
                subjects.TotalPages,
                subjects
            };

            return Ok(subjectsResult);
        }


        [HttpGet("{Id}", Name = "SubjectById")]
        public async Task<IActionResult> GetSubjectById(int Id)
        {           
            var subjectDto = await _service.GetSubjectByIdAsync(Id);

            if (subjectDto == null)
            {
                return NotFound();
            }
            return Ok(subjectDto);                       
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] Subject subjectFromBody)
        {  
            var createrSubject = await _service.CreateSubjectAsync(subjectFromBody);            

            return CreatedAtRoute("SubjectById", new { id = createrSubject.SubjectId }, createrSubject);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(int Id, [FromBody] SubjectPut subjectPut)
        {  
            var isUpdated = await _service.UpdateSubjectAsync(Id, subjectPut);

            if (!isUpdated)
            {                
                return NotFound();
            }            
            
            return NoContent();           
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {            
            var isDeleted = await _service.DeleteSubjectAsync(Id);

            if (!isDeleted)
            {                
                return NotFound();
            }            

            return NoContent();                        
        }
    }    
}
