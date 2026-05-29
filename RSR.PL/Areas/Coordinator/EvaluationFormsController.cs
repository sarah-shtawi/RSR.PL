using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSR.BLL.Service.EvaluationService;
using RSR.BLL.Services.EvaluationService;
using RSR.DAL.DTOs.Request.EvaluationRequest;

namespace RSR.PL.Areas.Coordinator
{
    [Area("Coordinator")]
    [ApiController]
    [Route("api/Coordinator/[controller]")]
    [Authorize(Roles = "Coordinator")]
    public class EvaluationFormsController : ControllerBase
    {
        private readonly IEvaluationFormService _formService;

        private readonly IEvaluationFieldService _fieldService;

        private readonly IFinalEvaluationResultService
            _finalResultService;

        public EvaluationFormsController(
            IEvaluationFormService formService,
            IEvaluationFieldService fieldService,
            IFinalEvaluationResultService finalResultService)
        {
            _formService = formService;

            _fieldService = fieldService;

            _finalResultService = finalResultService;
        }

        // =========================
        // CREATE FORM
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateEvaluationFormRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _formService.CreateAsync(request);

            return Ok(result);
        }

        // =========================
        // ADD FIELD TO FORM
        // =========================
        [HttpPost("{formId}/fields")]
        public async Task<IActionResult> AddField(
            int formId,
            [FromBody] CreateEvaluationFieldRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _fieldService.CreateAsync(
                    request,
                    formId);

            if (result == null)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot add field (form not found or not editable)"
                });
            }

            return Ok(result);
        }

        // =========================
        // GET FORM BY ID
        // =========================
        [HttpGet("{formId:int}")]
        public async Task<IActionResult> GetById(
            int formId)
        {
            var result =
                await _formService.GetByIdAsync(formId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Evaluation form not found"
                });
            }

            return Ok(result);
        }

        // =========================
        // GET ALL PUBLISHED FORMS
        // =========================
        [HttpGet("published")]
        public async Task<IActionResult>
            GetPublishedForms()
        {
            var result =
                await _formService
                    .GetPublishedFormsAsync();

            return Ok(result);
        }

        // =========================
        // UPDATE FORM
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateForm(
            int id,
            [FromBody]
            UpdateEvaluationFormRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _formService
                    .UpdateAsync(id, request);

            if (result == null)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot update form (not found, invalid data, or archived)"
                });
            }

            return Ok(result);
        }

        // =========================
        // DELETE FIELD
        // =========================
        [HttpDelete("fields/{id}")]
        public async Task<IActionResult>
            DeleteField(int id)
        {
            var result =
                await _fieldService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot delete field (not found or form is locked)"
                });
            }

            return Ok(new
            {
                message =
                    "Field deleted successfully"
            });
        }

        // =========================
        // UPDATE FIELD
        // =========================
        [HttpPut("fields/{id}")]
        public async Task<IActionResult>
            UpdateField(
            int id,
            [FromBody]
            UpdateEvaluationFieldRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _fieldService
                    .UpdateAsync(id, request);

            if (result == null)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot update field (not found, invalid data, or form is locked)"
                });
            }

            return Ok(result);
        }

        // =========================
        // PUBLISH FORM
        // =========================
        [HttpPost("{id}/publish")]
        public async Task<IActionResult>
            Publish(int id)
        {
            var result =
                await _formService.PublishAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Form cannot be published"
                });
            }

            return Ok(new
            {
                message =
                    "Form published successfully"
            });
        }

        // =========================
        // SAVE AS DRAFT
        // =========================
        [HttpPost("{id}/draft")]
        public async Task<IActionResult>
            SaveAsDraft(int id)
        {
            var result =
                await _formService.SetDraftAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot set form to Draft"
                });
            }

            return Ok(new
            {
                message =
                    "Saved as Draft successfully"
            });
        }

        // =========================
        // ARCHIVE FORM
        // =========================
        [HttpPost("{id}/archive")]
        public async Task<IActionResult>
            Archive(int id)
        {
            var result =
                await _formService.ArchiveAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot archive form"
                });
            }

            return Ok(new
            {
                message =
                    "Form archived successfully"
            });
        }

        // =========================
        // DELETE FORM
        // =========================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            DeleteForm(int id)
        {
            var result =
                await _formService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Cannot delete form (not found or not Draft)"
                });
            }

            return Ok(new
            {
                message =
                    "Form deleted successfully"
            });
        }

        // =========================
        // GENERATE FINAL GRADE
        // =========================
        [HttpPost("final-grade/{groupId}")]
        public async Task<IActionResult>
            GenerateFinalGrade(Guid groupId)
        {
            try
            {
                var result =
                    await _finalResultService
                        .GenerateAsync(groupId);

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

        // =========================
        // PUBLISH FINAL GRADE
        // =========================
        [HttpPost("final-grade/publish/{id}")]
        public async Task<IActionResult>
            PublishFinalGrade(int id)
        {
            var result =
                await _finalResultService
                    .PublishAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Final result not found"
                });
            }

            return Ok(new
            {
                message =
                    "Final result published successfully"
            });
        }

         // SET FINAL GRADE AS DRAFT
         [HttpPost("final-grade/draft/{id}")]
        public async Task<IActionResult>
            SetFinalGradeDraft(int id)
        {
            var result =
                await _finalResultService
                    .SetDraftAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Final result not found"
                });
            }

            return Ok(new
            {
                message =
                    "Final result moved to draft"
            });
        }

       
        // GET PUBLISHED FINAL GRADE
         [HttpGet("final-grade/{groupId}")]
        public async Task<IActionResult>
            GetPublishedFinalGrade(Guid groupId)
        {
            try
            {
                var result =
                    await _finalResultService
                        .GetPublishedResultAsync(groupId);

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

         // DASHBOARD STATISTICS
         [HttpGet("statistics")]
        public async Task<IActionResult>
            GetStatistics()
        {
            try
            {
                var result =
                    await _formService.GetDashboardStatisticsAsync();

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
       
        // GET ALL FINAL RESULTS
         [HttpGet("final-results")]
        public async Task<IActionResult>
            GetAllFinalResults()
        {
            try
            {
                var result =
                    await _finalResultService
                        .GetAllFinalResultsAsync();

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