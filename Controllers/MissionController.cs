using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Tools;

namespace MossadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly DbServer _context;
        private readonly AgentsController _agentsController;
        private readonly AgentService _agentService;
        private readonly MissionService _missionService;

        public MissionsController(DbServer context)
        {
            _context = context;
        }

        public MissionsController(AgentsController agentsController)
        {
            _agentsController = agentsController;
        }

        public MissionsController(AgentService agentService)
        {
            _ = agentService;
        }

        public MissionsController(MissionService missionService)
        {
            _missionService = missionService;
        }


        //רשימת משימות - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllMissions()
        {
            var missionsList = await _context.MissionsList.ToArrayAsync();
            return Ok(missionsList);
        }


        //יצירת משימה - מנהל בקרה בלבד
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMission(Guid id)
        {
            var resolt = await _missionService.CreateMissionS(id);
            return Ok(resolt);
        }


        // עדכון משימה - מנהל בקרה בלבד
        [HttpPost("update")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateMission(Guid id)
        {
            var resolt = _missionService.UpdateMissionS(id);
            return Ok(resolt);
        }
    }
}
