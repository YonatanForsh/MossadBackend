using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Enums;
using MossadBackend.Directions;
using MossadBackend.Tools;
using Microsoft.EntityFrameworkCore;

namespace MossadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly DbServer _context;
        private readonly SetMission _SetMission;

        public AgentsController(DbServer context)
        {
            _context = context;
        }

        public AgentsController(SetMission setMission)
        {
            _SetMission = setMission;
        }


        //רשימת סוכנים - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var agentsList = await _context.AgentsList.ToArrayAsync();
            return Ok(agentsList);
        }


        //יצירת סוכן - שרת סימולציה בלבד
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateAgents(Agent agent)
        {
            agent.Id = Guid.NewGuid();
            await InitAgentLocation(agent.Id);
            agent.Status = Enums.AgentEnum.AgentStatus.Passive.ToString();
            _context.AgentsList.Add(agent);
            _context.SaveChanges();
            await _SetMission.OfferMission();
            return Ok();
        }


        //אתחול מיקום סוכן - נוצר על ידי יצירת סוכן
        [HttpPut("agents/{id}/pin")]
        public async Task<IActionResult> InitAgentLocation(Guid id)
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
        [HttpPut("{id}/move")]
        [Produces("application/json")]
        public async Task<IActionResult> MoveAgent(Guid id, string direction)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            if (ExistAgent != null) 
            {        
                string[] parts = direction.Split(new[] { '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
                string dir = parts[1].Trim();
                List<int> values = new List<int>();
                if (Direction._direction.TryGetValue(dir, out values))
                {
                    ExistAgent.X += values[0];
                    ExistAgent.Y += values[1];
                }
            }
            return Ok(ExistAgent);
        }
    }
}
