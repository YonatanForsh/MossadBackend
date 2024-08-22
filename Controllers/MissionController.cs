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
        private readonly AgentsController _agentsController;

        public MissionsController(DbServer context)
        {
            _context = context;
        }

        public MissionsController(AgentsController agentsController)
        {
            _agentsController = agentsController;
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
            Mission mission = _context.MissionsList.FirstOrDefault(x => x.Id == id);
            Agent agent = mission.Agent;
            Target target = mission.Target;
            if (Math.Sqrt(Math.Pow(Convert.ToDouble(target.X - agent.X), 2) + Math.Pow(Convert.ToDouble(target.Y - agent.Y), 2)) < 200 && agent.Status != "Active" && target.Status == "Live")
            {
                mission.Status = Enums.MissionEnum.MissionStatus.OnAMission.ToString();
                mission.Agent.Status = Enums.AgentEnum.AgentStatus.Active.ToString();
                mission.Target.Status = Enums.TargetEnum.TargetStatus.Tracking.ToString();
                _context.MissionsList.Add(mission);
                _context.SaveChanges();
            }
            return Ok(mission);
        }

        // עדכון משימה - מנהל בקרה בלבד
        [HttpPost("update")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateMission(Guid id)
        {
            string D = "Don't Move!";
            foreach (var mission in _context.MissionsList)
            {
                if (mission.Status == Enums.MissionEnum.MissionStatus.OnAMission.ToString())
                {
                    switch ((mission.Agent.X, mission.Agent.Y, mission.Target.X, mission.Target.Y))
                    {
                        case var (xA, yA, xT, yT) when xA < xT && yA < yT:
                            D = "ne"; 
                            break;

                        case var (xA, yA, xT, yT) when xA < xT && yA == yT:
                            D = "e";
                            break;
                        case var (xA, yA, xT, yT) when xA < xT && yA > yT:
                            D = "se";
                            break;

                        case var (xA, yA, xT, yT) when xA == xT && yA < yT:
                            D = "n";
                            break;

                        case var (xA, yA, xT, yT) when xA == xT && yA > yT:
                            D = "s";
                            break;

                        case var (xA, yA, xT, yT) when xA > xT && yA > yT:
                            D = "sw";
                            break;

                        case var (xA, yA, xT, yT) when xA > xT && yA == yT:
                            D = "w";
                            break;
                        case var (xA, yA, xT, yT) when xA > xT && yA < yT:
                            D = "nw";
                            break;

                        default:
                            D = "Don't Move!";
                            break;
                    }
                }
            }
            return Ok(D);
        }
    }
}
