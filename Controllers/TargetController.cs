using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Directions;
using MossadBackend.Models;

namespace MossadBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly DbServer _context;

        public TargetController(DbServer context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTargets()
        {
            return Ok(_context.TargetesList.ToList());
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateTarget(Target target)
        {
            target.Id = Guid.NewGuid();
            target.X = 30;
            target.Y = 40;
            target.Status = Enums.TargetEnum.TargetStatus.Live.ToString();
            _context.TargetesList.Add(target);
            _context.SaveChanges();
            return Ok();
        }

        //[HttpPut("{id}/pin")]
        //public async Task<IActionResult> InitTargetLocation(Guid id)
        //{
        //    Target ExistTarget = _context.TargetesList.FirstOrDefault(x => x.Id == id);
        //    if (ExistTarget != null)
        //    {
        //        Locations newLocation = new Locations(30, 40);
        //        ExistTarget.Location = newLocation;
        //        _context.SaveChanges();
        //    }
        //    return Ok(ExistTarget);
        //}

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
