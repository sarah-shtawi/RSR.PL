using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.GroupService;
using RSR.BLL.Service.Users;
using RSR.DAL.DTOs.Response.GroupRes;

namespace RSR.PL.Areas.Coordinator
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController( IGroupService groupService)
        {
            _groupService = groupService;
        }

        [Authorize(Roles = "Coordinator")]
        [HttpGet("groups-coordinater")]
        public async Task <IActionResult> GetCoordinatersGroups()
        {
            var result = await _groupService.GetCoordinatersGroups();
             return Ok( new { message = "success", AllSupervisorsWithGroups = result });
        }

        [HttpGet("group/{groupId}")]
        [Authorize(Roles =("Supervisor,Coordinator"))]
        public async Task<IActionResult> GetGroupById([FromRoute] Guid groupId)
        {
            var group = await _groupService.GetGroupById(groupId);
            if (group == null)
            {
                return BadRequest(group);            
            }
            return Ok(new { message = "success", group });
        }

    }
}
