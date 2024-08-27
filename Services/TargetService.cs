using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Directions;
using MossadBackend.Models;

namespace MossadBackend.Tools
{
    public class TargetService : Controller
    {
        private readonly DbServer _context;
        private readonly SetMission _SetMission;
        private readonly MissionService _missionService;


        public TargetService(DbServer context, SetMission setMission, MissionService missionService)
        {
            _context = context;
            _SetMission = setMission;
            _missionService = missionService;
        }


        //רשימת מטרות - ללא מגבלת הרשאות
        
        public async Task<List<Target>> GetAllTargetsS()
        {
            var targetsList = await _context.TargetesList.ToListAsync();
            return targetsList;
        }


        //יצירת מטרה - שרת סימולציה בלבד
        public async Task<Target> CrateTargetS(Target target)
        {
            target.Id = Guid.NewGuid();
            target.Status = Enums.TargetEnum.TargetStatus.Live.ToString();
            _context.TargetesList.Add(target);
            _context.SaveChanges();
            await InitTargetLocation(target.Id);
            _context.SaveChanges();
            await _missionService.OfferMissionS();
            return target;
        }


        //אתחול מיקום מטרה - נוצר על ידי יצירת מטרה
        [HttpPut("{id}/pin")]
        public async Task<Target> InitTargetLocation(Guid id)
        {
            Target ExistTarget = _context.TargetesList.FirstOrDefault(x => x.Id == id);
            if (ExistTarget != null)
            {
                ExistTarget.X = 35;
                ExistTarget.Y = 45;
                _context.SaveChanges();
            }
            return ExistTarget;
        }


        //הזזת מיקום מטרה - שרת סימולציה בלבד
        [HttpPut("{id}/move")]
        [Produces("application/json")]
        public async Task<Target> MoveTargetS(Guid id, string direction)
        {
            Target ExistTarget = _context.TargetesList.FirstOrDefault(x => x.Id == id);
            if (ExistTarget != null)
            {
                string[] parts = direction.Split(new[] { '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
                string dir = parts[1].Trim();
                List<int> values = new List<int>();
                if (Direction._direction.TryGetValue(dir, out values))
                {
                    ExistTarget.X += values[0];
                    ExistTarget.Y += values[1];
                }
            }
            return ExistTarget;
        }
    }
}
