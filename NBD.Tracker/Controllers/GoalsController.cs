using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NBD.SDK;
using NBD.Services.Core.Exceptions;
using NBD.Services.Goals;
using NBD.Tracker.Models;
using System;
using System.Threading.Tasks;

namespace NBD.Tracker.Controllers
{
    [EnableCors("UIPolicy")]
    [Route("api/[controller]")]
    public class GoalsController : Controller
    {
        private readonly IGoalsService goalsService;

        public GoalsController(IGoalsService goalsService)
        {
            this.goalsService = goalsService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGoalAsync(Guid id)
        {
            try
            {
                var goal = await this.goalsService.GetGoalAsync(id);
                var model = Mapper.Map<Goal, GoalViewModel>(goal);

                return Ok(model);
            }
            catch (GoalNotFoundException gnfe)
            {
                return NotFound(gnfe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddGoalAsync([FromBody]GoalBindingModel model)
        {
            try
            {
                if (!(ModelState.IsValid && await model.IsWithinParentDatesAsync(this.goalsService)))
                {
                    return BadRequest(model);
                }

                var goal = Mapper.Map<GoalBindingModel, Goal>(model);

                await this.goalsService.AddGoalAsync(goal);

                return Ok(goal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGoalAsync(Guid id)
        {
            try
            {
                await this.goalsService.DeleteCascadingAsync(id);

                return Ok();
            }
            catch (GoalNotFoundException gnfe)
            {
                return NotFound(gnfe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditGoalAsync([FromBody]GoalEditModel model)
        {
            try
            {
                if (!(ModelState.IsValid && await model.IsWithinParentDatesAsync(this.goalsService)))
                {
                    return BadRequest(model);
                }

                var goal = Mapper.Map<GoalEditModel, Goal>(model);

                await this.goalsService.EditGoalAsync(goal);

                return Ok(goal);
            }
            catch (GoalNotFoundException gnfe)
            {
                return NotFound(gnfe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("progress")]
        public async Task<IActionResult> MakeProgressAsync([FromBody]ProgressBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                var progressedGoal =
                    await this.goalsService.MakeProgressAsync(model.GoalId, model.Progress);

                return Ok(progressedGoal);
            }
            catch (GoalNotFoundException gnfe)
            {
                return NotFound(gnfe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("reorder")]
        public async Task<IActionResult> ReorderTreeAsync([FromBody]ReorderModel model)
        {
            try
            {
                if (!(ModelState.IsValid && await model.IsValidReorderAsync(this.goalsService)))
                {
                    return BadRequest(model);
                }

                var movedGoal = await this.goalsService.ReorderTreeAsync(
                    model.GoalId, model.TargetParentId);

                return Ok(movedGoal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
