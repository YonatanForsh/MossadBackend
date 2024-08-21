using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Enums;

namespace MossadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly DbServer _context;

        public AgentsController(DbServer context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            return Ok(_context.AgentsList.ToList());            
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateAgents(Agent agent)
        {
            agent.Id = Guid.NewGuid();
            agent.Status = Enums.AgentEnum.AgentStatus.Passive.ToString();
            _context.AgentsList.Add(agent);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("agents/{id}/pin")]
        public async Task<IActionResult> InitAgentLocation(Guid id)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x =>  x.Id == id);
            if (ExistAgent != null)
            {
                Locations newLocation = new Locations(30, 40);
                ExistAgent.Location = newLocation;
                _context.SaveChanges();
            }
            return Ok(ExistAgent);
        }


        [HttpPut("agents/{id}/move")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAgentLocation(Guid id, Locations location)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            string ExistAgentStatus = ExistAgent.Status.ToString();
            if (ExistAgentStatus == "Active") return NotFound("The agent already active");
            if (ExistAgent != null)
            {
                ExistAgent.Location = location;
            }
            return Ok(ExistAgent);
        }
    }
}
