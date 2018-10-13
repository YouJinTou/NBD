using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NBD.SDK;
using NBD.Tracker.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using NBD.Services.Goals;
using NBD.Services.Core.Exceptions;

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
                if (!(ModelState.IsValid && await model.IsWithinParentDatesAsync(this.goals)))
                {
                    return BadRequest(model);
                }

                var goal = this.goals.Where(g => g.Id == model.Id).FirstOrDefault();

                if (goal == null)
                {
                    return NotFound(goal);
                }

                goal.Target = model.Target;
                goal.StartDate = model.StartDate;
                goal.EndDate = model.EndDate;
                goal.Description = model.Description;
                goal.Title = model.Title;
                goal.RecurrenceType = model.RecurrenceType;
                goal.RecurrenceValue = model.RecurrenceValue;

                await this.goals.EditAsync(goal);

                return Ok(goal);
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

                var goal = this.goals.Where(g => g.Id == model.GoalId).FirstOrDefault();

                if (goal == null)
                {
                    return NotFound(goal);
                }

                goal.MakeProgress(model.Progress);

                await this.goals.EditAsync(goal);

                return Ok(goal);
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
                if (!(ModelState.IsValid && await model.IsValidReorderAsync(this.goals)))
                {
                    return BadRequest(model);
                }

                var goal = await this.goals.GetAsync(model.GoalId);
                goal.ParentId = model.TargetParentId;

                await this.goals.EditAsync(goal);

                return Ok(goal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
