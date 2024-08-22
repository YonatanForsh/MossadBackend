using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;

namespace MossadBackend.Tools
{
    public class SetMission
    {
        private readonly DbServer _context;


        public SetMission(DbServer context)
        {
            _context = context;
        }
        public void Set()
        {
            var targets = _context.TargetesList.ToList();
            var agents = _context.AgentsList.ToList();
            foreach (var target in targets)
            {
                foreach (var agent in agents)
                {
                    if (Math.Sqrt(Math.Pow(target.X - agent.X, 2) + Math.Pow(target.Y - agent.Y, 2)) < 200)
                    {
                        Mission mission = new Mission();
                        mission.Status = "possible";
                        mission.AgentId = agent.Id;
                        mission.TargetId = target.Id;
                        _context.Mission.Add(mission);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
