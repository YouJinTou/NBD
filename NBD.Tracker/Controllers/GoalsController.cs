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
                    var goals = this.goals.Where(g => g.Id == id);
                    var modelGoals = Mapper.Map<IEnumerable<Goal>, IEnumerable<GoalViewModel>>(goals);
                    var modelRoot = modelGoals.FirstOrDefault(mg => mg.Id == id);

                    return Ok(modelRoot);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
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
                return StatusCode(500, ex);
            }
        }
    }
}
