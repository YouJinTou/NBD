using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NBD.SDK;
using NBD.Tracker.DAL;
using NBD.Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Tracker.Controllers
{
    [EnableCors("UIPolicy")]
    [Route("api/[controller]")]
    public class GoalsController : Controller
    {
        private readonly IRepository<Goal> goals;

        public GoalsController(IRepository<Goal> goals)
        {
            this.goals = goals;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGoalAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var goal = this.goals.Where(g => g.Id == id).FirstOrDefault();

                    if (goal == null)
                    {
                        return NotFound(goal);
                    }

                    var modelRoot = Mapper.Map<Goal, GoalViewModel>(goal);

                    return Ok(modelRoot);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddGoalAsync([FromBody]GoalBindingModel model)
        {
            try
            {
                if (!(ModelState.IsValid && await model.IsWithinParentDatesAsync(this.goals)))
                {
                    return BadRequest(model);
                }

                var goal = Mapper.Map<GoalBindingModel, Goal>(model);

                await this.goals.AddAsync(goal);

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
    }
}
