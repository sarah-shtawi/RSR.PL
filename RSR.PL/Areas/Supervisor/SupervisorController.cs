using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.EvaluationService;
using System.Security.Claims;

namespace RSR.PL.Areas.Supervisor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Supervisor")]
    public class SupervisorController : ControllerBase
    {
        private readonly IFinalEvaluationResultService
            _finalResultService;

        public SupervisorController(
            IFinalEvaluationResultService finalResultService)
        {
            _finalResultService =
                finalResultService;
        }

        //  
        // GET SUPERVISOR GROUPS
        // FINAL GRADES
        // =========================
        [HttpGet("groups-final-grades")]
        public async Task<IActionResult>
            GetGroupsFinalGrades()
        {
            try
            {
                var supervisorId =
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier);

                var result =
                    await _finalResultService
                        .GetSupervisorGroupsFinalGradesAsync(
                            supervisorId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
