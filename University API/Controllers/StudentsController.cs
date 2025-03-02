using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Dtos.StudentDtos;
using Service.Interfaces;
using UniversityAppAgain.Dtos.StudentDtos;

namespace University_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("")]
        public ActionResult Create(StudentCreateDto studentCreateDto)
        {
            var id = _studentService.Create(studentCreateDto);
            return StatusCode(201, new { id });
        }

        [HttpDelete("")]
        public ActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, StudentUpdateDto studentUpdateDto)
        {
            _studentService.Edit(id, studentUpdateDto);
            return NoContent();
        }

        [HttpGet("all-students")]
        public ActionResult<List<StudentGetDto>> GetAll(string? search = null)
        {
            return StatusCode(200, _studentService.GetAll(search));
        }

        [HttpGet("")]
        public ActionResult<PaginatedList<StudentGetDto>> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            return StatusCode(200, _studentService.GetAllByPage(search, page, size));
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> GetById(int id)
        {
            return StatusCode(200, _studentService.GetById(id));
        }
    }
}
