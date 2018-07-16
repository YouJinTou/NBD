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
    public class GoalTreesController : Controller
    {
        private readonly IRepository<GoalTree> trees;
        private readonly IRepository<Goal> goals;

        public GoalTreesController(IRepository<GoalTree> trees, IRepository<Goal> goals)
        {
            this.trees = trees;
            this.goals = goals;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTreeAsync(Guid id)
        {
            try
            {
                var tree = await this.trees.GetAsync(id);
                var goals = this.goals.Where(g => g.RootId == tree.RootId);
                var modelGoals = Mapper.Map<IEnumerable<Goal>, IEnumerable<GoalViewModel>>(goals);
                var modelRoot = modelGoals.FirstOrDefault(mg => mg.Id == tree.RootId);

                return Ok(modelRoot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTreeAsync([FromBody]GoalTreeBindingModel model)
        {
            try
            {
                var goal = Mapper.Map<GoalBindingModel, Goal>(model.RootGoal);
                var tree = Mapper.Map<GoalTreeBindingModel, GoalTree>(model);
                goal.RootId = goal.Id;
                tree.RootId = goal.Id;

                await this.goals.AddAsync(goal);

                await this.trees.AddAsync(tree);

                return Ok(tree);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
