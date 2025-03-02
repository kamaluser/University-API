using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.GroupDtos;
using Service.Dtos;
using Service.Interfaces;

namespace University_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("")]
        public ActionResult Create(GroupCreateDto createDto)
        {
            var id = _groupService.Create(createDto);
            return StatusCode(201, new { id });
        }

        [HttpGet("")]
        public ActionResult<PaginatedList<GroupGetDto>> GetAll(string? search = null, int page = 1, int size = 10)
        {
            return StatusCode(200, _groupService.GetAllByPage(search, page, size));
        }

        [HttpGet("all-groups")]
        public ActionResult<List<GroupGetDto>> GetAllGroups(string? search = null)
        {
            return StatusCode(200, _groupService.GetAll(search));
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            var result = _groupService.GetById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, GroupUpdateDto editDto)
        {
            _groupService.Edit(id, editDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _groupService.Delete(id);
            return NoContent();
        }
    }
}
