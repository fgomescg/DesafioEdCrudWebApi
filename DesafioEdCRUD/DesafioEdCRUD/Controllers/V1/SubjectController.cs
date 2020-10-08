using System.Threading.Tasks;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEdCRUD.Controllers.V1
{    
    [ApiController]   
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet(ApiRoutes.Subjects.GetAll)]
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


        [HttpGet(ApiRoutes.Subjects.Get, Name = "SubjectById")]
        public async Task<IActionResult> GetSubjectById(int subjectId)
        {           
            var subjectDto = await _service.GetSubjectByIdAsync(subjectId);

            if (subjectDto == null)
            {
                return NotFound();
            }
            return Ok(subjectDto);                       
        }

        [HttpPost(ApiRoutes.Subjects.Create)]
        public async Task<IActionResult> CreateSubject([FromBody] Subject subjectFromBody)
        {  
            var createrSubject = await _service.CreateSubjectAsync(subjectFromBody);            

            return CreatedAtRoute("SubjectById", new { subjectId = createrSubject.SubjectId }, createrSubject);            
        }

        [HttpPut(ApiRoutes.Subjects.Update)]
        public async Task<IActionResult> UpdateSubject(int subjectId, [FromBody] SubjectPut subjectPut)
        {  
            var isUpdated = await _service.UpdateSubjectAsync(subjectId, subjectPut);

            if (!isUpdated)
            {                
                return NotFound();
            }            
            
            return NoContent();           
        }

        [HttpDelete(ApiRoutes.Subjects.Delete)]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {            
            var isDeleted = await _service.DeleteSubjectAsync(subjectId);

            if (!isDeleted)
            {                
                return NotFound();
            }            

            return NoContent();                        
        }
    }    
}
