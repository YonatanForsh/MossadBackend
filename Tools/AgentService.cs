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
    public class AgentService
    {
        private readonly DbServer _context;
        //private readonly SetMission _SetMission;
        private readonly SetMission _setMission;


        public AgentService(DbServer context, SetMission setMission)
        {
            // SetMission setMission,
            _context = context;
            _setMission = setMission;
            //_SetMission = setMission;
        }


        //רשימת סוכנים - ללא מגבלת הרשאות
        public async Task<List<Agent>> GetAllAgentsS()
        {
            var agentsList = await _context.AgentsList.ToListAsync();
            return agentsList;
        }


        //יצירת סוכן - שרת סימולציה בלבד
        [HttpPost]
        public async Task<Agent> CrateAgentsS(Agent agent)
        {
            agent.Id = Guid.NewGuid();
            agent.Status = Enums.AgentEnum.AgentStatus.Passive.ToString();
            _context.AgentsList.Add(agent);
            await InitAgentLocationS(agent.Id);
            _context.SaveChanges();
            await _setMission.OfferMissionS();
            return agent;
        }


        //אתחול מיקום סוכן - נוצר על ידי יצירת סוכן
        [HttpPut("agents/{id}/pin")]
        public async Task<Agent> InitAgentLocationS(Guid id)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            if (ExistAgent != null)
            {
                ExistAgent.X = 30;
                ExistAgent.Y = 40;
                _context.SaveChanges();
            }
            return ExistAgent;
        }



        //הזזת מיקום סוכן - שרת סימולציה בלבד
        public async Task<Agent> MoveAgentS(Guid id, string direction)
        {
            Agent ExistAgent = _context.AgentsList.FirstOrDefault(x => x.Id == id);
            if (ExistAgent != null)
            {
                string[] parts = direction.Split(new[] { '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
                string dir = parts[1].Trim();
                List<int> values = new List<int>();
                if (Direction._direction.TryGetValue(direction, out values))
                {
                    if (ExistAgent.X < 1000 && ExistAgent.Y < 1000)
                    {
                        ExistAgent.X += values[0];
                        ExistAgent.Y += values[1];
                    }
                }
                //חיסול - כאשר סוכן ומטרה נפגשים
                Mission mission = _context.MissionsList.FirstOrDefault(x => x.Id == ExistAgent.Id);
                if (ExistAgent.X == mission.Target.X && ExistAgent.Y == mission.Target.Y)
                {
                    ExistAgent.Status = "Passive";
                    mission.Target.Status = "Dead";
                    mission.Status = "Finished";
                }
            }
            return ExistAgent;
        }
    }
}
