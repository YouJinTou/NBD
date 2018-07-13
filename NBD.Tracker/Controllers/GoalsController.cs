using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NBD.SDK;
using NBD.Tracker.DAL;
using NBD.Tracker.Models;
using System;
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
            try
            {
                var goal = await this.goals.GetAsync(id);
                var model = Mapper.Map<Goal, GoalViewModel>(goal);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddGoalAsync([FromBody]GoalBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
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
