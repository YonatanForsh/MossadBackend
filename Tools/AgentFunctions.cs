//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MossadBackend.Tools;
//using MossadBackend.Models;
//using MossadBackend.Controllers;
//using MossadBackend.DB;
//using Microsoft.AspNetCore.Http.HttpResults;

//namespace MossadBackend.Tools
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AgentFunctions : ControllerBase
//    {
//        private readonly DbServer _context;

//        public AgentFunctions(DbServer context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllAgentsf()
//        {
//            var agentsList = await _context.AgentsList.ToArrayAsync();
//            return Ok(agentsList);  
//        }

//        [HttpPost]
//        public async Task<IActionResult> CrateAgentsf(Agent agent)
//        {
//            agent.Id = Guid.NewGuid();
//            agent.Status = Enums.AgentEnum.AgentStatus.Passive.ToString();
//            _context.AgentsList.Add(agent);
//            _context.SaveChanges();
//            return Ok();
//        }

//    }
//}
