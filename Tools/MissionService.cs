using Microsoft.AspNetCore.Mvc;
using MossadBackend.DB;
using MossadBackend.Models;

namespace MossadBackend.Tools
{
    public class MissionService : Controller
    {
        private readonly DbServer _context;
        private readonly AgentService _agentService;


        public MissionService(DbServer context, AgentService agentService)
        {
            _context = context;
            _agentService = agentService;
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> OfferMissionS()
        {
            var targets = _context.TargetesList.ToList();
            var agents = _context.AgentsList.ToList();
            if (targets != null && agents != null)
            {
                foreach (var target in targets)
                {
                    foreach (var agent in agents)
                    {
                        if (Math.Sqrt(Math.Pow(Convert.ToDouble(target.X - agent.X), 2) + Math.Pow(Convert.ToDouble(target.Y - agent.Y), 2)) < 200 && agent.Status != "Active" && target.Status == "Live")
                        {
                            var time = Math.Sqrt(Math.Pow(Convert.ToDouble(target.X - agent.X), 2) + Math.Pow(Convert.ToDouble(target.Y - agent.Y), 2) / 5);
                            Mission mission = new Mission();
                            mission.Id = new Guid();
                            mission.Agent = agent;
                            mission.Target = target;
                            mission.Time = time;
                            mission.killingTime = DateTime.Now;
                            mission.Status = Enums.MissionEnum.MissionStatus.Offer.ToString();
                        }
                    }
                }
            }
            _context.SaveChanges();
            return Ok(_context.MissionsList);
        }


        //יצירת משימה - מנהל בקרה בלבד
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMissionS(Guid id)
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
        public async Task<IActionResult> UpdateMissionS(Guid id)
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
            return Ok(_agentService.MoveAgentS(id, "{Direction: " + D));
        }
    }
}

