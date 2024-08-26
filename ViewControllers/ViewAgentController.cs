using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.ViewModel;
using MossadBackend.ViewModels;

namespace MossadBackend.ViewControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAgentController : ControllerBase
    {
        public readonly DbServer _context;
        public readonly ViewAgent _viewAgent;

        public ViewAgentController(DbServer context, ViewAgent viewAgent)
        {
            _context = context;
            _viewAgent = viewAgent;
        }

        //[HttpGet]
        //public IActionResult FillViewAgent()
        //{
        //    Agent agent = _context.AgentsList.FirstOrDefaultAsync(a => a.Id == _viewAgent.AgentId);
        //    Mission mission = _context.MissionsList.FirstOrDefaultAsync(m => m.Agent.Id == _viewAgent.AgentId);
        //    _viewAgent.Name = agent.Name;
        //    _viewAgent.X = agent.X;
        //    _viewAgent.Y = agent.Y;
        //    _viewAgent.Status = agent.Status;
        //    _viewAgent.MissionId = mission.Id;
        //    _viewAgent.killingTime = mission.killingTime;
        //    _viewAgent.Kills = agent.kills;
            
        //    return Ok(_viewAgent);
        //}
    }
}
