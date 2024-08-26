using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Tools;
using MossadBackend.ViewModel;

namespace MossadBackend.ViewControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewActiveMissionController : ControllerBase
    {
        public readonly DbServer _context;
        public readonly ViewActiveMissions _viewActiveMissions;

        public ViewActiveMissionController(DbServer context, ViewActiveMissions viewActiveMissions)
        {
            _context = context;
            _viewActiveMissions = viewActiveMissions;
        }

        [HttpGet]
        public IActionResult FillActiveMissionController()
        {
            _viewActiveMissions.
        }
    }
}
