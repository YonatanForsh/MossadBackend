using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Directions;
using MossadBackend.Models;
using MossadBackend.Tools;

namespace MossadBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly DbServer _context;
        private readonly SetMission _SetMission;


        public TargetController(DbServer context)
        {
            _context = context;
        }

        public TargetController(SetMission setMission)
        {
            _SetMission = setMission;
        }


        //רשימת מטרות - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllTargets()
        {
            return Ok(_context.TargetesList.ToList());
        }

        //יצירת מטרה - שרת סימולציה בלבד
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateTarget(Target target)
        {
            target.Id = Guid.NewGuid();
            await InitTargetLocation(target.Id);
            target.Status = Enums.TargetEnum.TargetStatus.Live.ToString();
            _context.TargetesList.Add(target);
            _context.SaveChanges();
            await _SetMission.OfferMission();
            return Ok();
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
        public async Task<IActionResult> MoveTarget(Guid id, string direction)
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
