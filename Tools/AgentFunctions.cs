using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.Tools;
using MossadBackend.Models;
using MossadBackend.Controllers;
using MossadBackend.DB;

namespace MossadBackend.Tools
{

    public class AgentFunctions
    {
        private readonly DbServer _context;

        public AgentFunctions(DbServer context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetAllAgents()
        {
            return _context.AgentsList.ToList();
        }

    }
}
