using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
       
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public SubjectController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects([FromQuery] SubjectParameters subjectParameters)
        {
            try
            {
                var Subjects = await _repository.Subject.GetAllSubjects(subjectParameters);

                _logger.LogInfo($"Returned all Subjects from database.");

                var SubjectsResult = _mapper.Map<IEnumerable<SubjectDto>>(Subjects);
                return Ok(SubjectsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllSubjectss: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{Id}", Name = "SubjectById")]
        public async Task<IActionResult> GetSubjectById(int Id)
        {
            try
            {
                var subject = await _repository.Subject.GetSubjectById(Id);

                if (subject == null)
                {
                    _logger.LogError($"Subject with id:{Id}, not found.");
                    return NotFound();
                }
                else
                {
                    var subjectResult = _mapper.Map<SubjectDto>(subject);
                    _logger.LogInfo($"Returned Subject with id: {Id}");
                    return Ok(subjectResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetSubjectById: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateSubject([FromBody] SubjectForCreationDto subject)
        {
            try
            {
                if (subject == null)
                {
                    _logger.LogError("Subject object sent from client is null.");
                    return BadRequest("Subject object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Subject object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var subjectEntity = _mapper.Map<Subject>(subject);

                _repository.Subject.CreateSubject(subjectEntity);
                _repository.Subject.Save();

                var createdSubject = _mapper.Map<SubjectDto>(subjectEntity);

                return CreatedAtRoute("SubjectById", new { id = createdSubject.SubjectId }, createdSubject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateSubject: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(int Id, [FromBody] Subject subjectObj)
        {
            try
            {
                if (subjectObj == null)
                {
                    _logger.LogError("Subject object sent from client is null.");
                    return BadRequest("Subject object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Subject object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var subjectEntity = await _repository.Subject.GetSubjectById(Id);

                if (subjectEntity == null)
                {
                    _logger.LogError($"Subject with id: {Id}, not found in db.");
                    return NotFound();
                }

                _mapper.Map(subjectObj, subjectEntity);

                _repository.Subject.UpdateSubject(subjectEntity);
                _repository.Subject.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateSubject: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            try
            {
                var subject = await _repository.Subject.GetSubjectById(Id);

                if (subject == null)
                {
                    _logger.LogError($"Subject with id: {Id}, not found in db.");
                    return NotFound();
                }

                _repository.Subject.DeleteSubject(subject);
                _repository.Subject.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteSubject action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }    
}
