using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.Tools;
using MossadBackend.Models;
using MossadBackend.Controllers;
using MossadBackend.DB;
using Microsoft.AspNetCore.Http.HttpResults;
using MossadBackend.Directions;

namespace MossadBackend.Tools
{
    public class AgentService : ControllerBase
    {
        private readonly DbServer _context;
        private readonly SetMission _SetMission;
        private readonly MissionService _missionService;


        public AgentService(DbServer context)
        {
            _context = context;
        }

        public AgentService(SetMission setMission)
        {
            _SetMission = setMission;
        }

        public AgentService(MissionService missionService)
        {
            _missionService = missionService;
        }



        //רשימת סוכנים - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllAgentsS()
        {
            var agentsList = await _context.AgentsList.ToArrayAsync();
            return Ok(agentsList);
        }


        //יצירת סוכן - שרת סימולציה בלבד
        [HttpPost]
        public async Task<IActionResult> CrateAgentsS(Agent agent)
        {
            agent.Id = Guid.NewGuid();
            await InitAgentLocationS(agent.Id);
            agent.Status = Enums.AgentEnum.AgentStatus.Passive.ToString();
            _context.AgentsList.Add(agent);
            _context.SaveChanges();
            await _missionService.OfferMissionS();
            return Ok(agent);
        }


        //אתחול מיקום סוכן - נוצר על ידי יצירת סוכן
        [HttpPut("agents/{id}/pin")]
        public async Task<IActionResult> InitAgentLocationS(Guid id)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            if (ExistAgent != null)
            {
                ExistAgent.X = 30;
                ExistAgent.Y = 40;
                _context.SaveChanges();
            }
            return Ok(ExistAgent);
        }



        //הזזת מיקום סוכן - שרת סימולציה בלבד
        public async Task<IActionResult> MoveAgentS(Guid id, string direction)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            if (ExistAgent != null)
            {
                string[] parts = direction.Split(new[] { '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
                string dir = parts[1].Trim();
                List<int> values = new List<int>();
                if (Direction._direction.TryGetValue(direction, out values))
                {
                    ExistAgent.X += values[0];
                    ExistAgent.Y += values[1];
                }
            }
            return Ok(ExistAgent);
        }
    }
}
