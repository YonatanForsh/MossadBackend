using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;


namespace MossadBackend.Tools
{
    public class SetMission: ControllerBase
    {
        private readonly DbServer _context;


        public SetMission(DbServer context)
        {
            _context = context;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> OfferMission()
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
    }
}

