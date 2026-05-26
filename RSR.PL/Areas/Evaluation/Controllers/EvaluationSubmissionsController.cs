using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.EvaluationService;
using RSR.DAL.DTOs.Request.EvaluationSubmissionRequest;

namespace RSR.PL.Areas.Evaluation.Controllers
{
    [Area("Evaluation")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Authorize(Roles = "Supervisor,Examiner")]
    public class EvaluationSubmissionsController
        : ControllerBase
    {
        private readonly IEvaluationSubmissionService
            _submissionService;

        public EvaluationSubmissionsController(
            IEvaluationSubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        // =========================
        // SUBMIT EVALUATION
        // =========================
        [HttpPost]
        public async Task<IActionResult> Submit(
    [FromBody] SubmitEvaluationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _submissionService
                    .SubmitAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        // =========================
        // GET MY FORMS
        // =========================
        [HttpGet("my-forms")]
        public async Task<IActionResult> GetMyForms()
        {
            try
            {
                var result = await _submissionService
                    .GetMyFormsAsync();

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