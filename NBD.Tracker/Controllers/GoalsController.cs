using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBD.SDK;
using NBD.Tracker.DAL;
using NBD.Tracker.Models;
using System;
using System.Threading.Tasks;

namespace NBD.Tracker.Controllers
{
    [Route("api/[controller]/")]
    public class GoalsController : Controller
    {
        private readonly ITrackerContext context;

        public GoalsController(ITrackerContext context)
        {
            this.context = context;
        }

        [Route("{id}")]
        public async Task<IActionResult> GetGoal(Guid id)
        {
            var goal = await this.context.Goals.FirstOrDefaultAsync(g => g.Id == id);
            var model = Mapper.Map<Goal, GoalViewModel>(goal);

            return Ok(model);
        }
    }
}
