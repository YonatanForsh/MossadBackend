using Microsoft.AspNetCore.Mvc;
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


        public TargetService(DbServer context)
        {
            _context = context;
        }

        public TargetService(SetMission setMission)
        {
            _SetMission = setMission;
        }

        public TargetService(MissionService missionService)
        {
            _missionService = missionService;
        }


        //רשימת מטרות - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllTargetsS()
        {
            return Ok(_context.TargetesList.ToList());
        }


        //יצירת מטרה - שרת סימולציה בלבד
        public async Task<IActionResult> CrateTargetS(Target target)
        {
            target.Id = Guid.NewGuid();
            await InitTargetLocation(target.Id);
            target.Status = Enums.TargetEnum.TargetStatus.Live.ToString();
            _context.TargetesList.Add(target);
            _context.SaveChanges();
            await _missionService.OfferMissionS();
            return Ok(target);
        }


        //אתחול מיקום מטרה - נוצר על ידי יצירת מטרה
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> InitTargetLocation(Guid id)
        {
            Target ExistTarget = _context.TargetesList.FirstOrDefault(x => x.Id == id);
            if (ExistTarget != null)
            {
                ExistTarget.X = 30;
                ExistTarget.Y = 40;
                _context.SaveChanges();
            }
            return Ok(ExistTarget);
        }


        //הזזת מיקום מטרה - שרת סימולציה בלבד
        [HttpPut("{id}/move")]
        [Produces("application/json")]
        public async Task<IActionResult> MoveTargetS(Guid id, string direction)
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
            return Ok(ExistTarget);
        }
    }
}
