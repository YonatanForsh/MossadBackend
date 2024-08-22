using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;

namespace MossadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly DbServer _context;

        public MissionsController(DbServer context)
        {
            _context = context;
        }

        //רשימת משימות - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllMissions()
        {
            var missionsList = await _context.MissionsList.ToArrayAsync();
            return Ok(missionsList);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMission(Mission mission)
        {
            mission.Status = Enums.MissionEnum.MissionStatus.OnAMission.ToString();
            mission.Agent.Status = Enums.AgentEnum.AgentStatus.Active.ToString();
            mission.Target.Status = Enums.TargetEnum.TargetStatus.Tracking.ToString();
            _context.MissionsList.Add(mission);
            _context.SaveChanges();
            return Ok(mission);

        }

    }
}
