using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Tools;
using MossadBackend.ViewModel;

namespace MossadBackend.ViewControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewActiveMissionController : ControllerBase
    {
        public readonly DbServer _context;
        public readonly ViewActiveMissions _viewActiveMissions;

        public ViewActiveMissionController(DbServer context, ViewActiveMissions viewActiveMissions)
        {
            _context = context;
            _viewActiveMissions = viewActiveMissions;
        }

        [HttpGet]
        public async Task<IActionResult> FillActiveMission()
        {
            Mission mission = _context.MissionsList.FirstOrDefaultAsync (m => m.Id == _viewActiveMissions.MissionId);
            _viewActiveMissions.AgentName = mission.Agent.Name;
            _viewActiveMissions.TargetName = mission.Target.Name;
            _viewActiveMissions.AgentXLocation = mission.Agent.X;
            _viewActiveMissions.AgentYLocation = mission.Agent.Y;
            _viewActiveMissions.TargetXLocation = mission.Target.X;
            _viewActiveMissions.TargetYLocation = mission.Target.Y;
            _viewActiveMissions.Distance = mission.Target.Y;
            _viewActiveMissions.killingTime = mission.killingTime;
            return Ok(_viewActiveMissions);
        }
    }
}
