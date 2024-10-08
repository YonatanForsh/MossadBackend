﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.Enums;
using MossadBackend.Directions;
using MossadBackend.Tools;
using Microsoft.EntityFrameworkCore;

namespace MossadBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        //מופעים של מחלקות ודאטה בייס להזרקה שלהם בקונטרולר (DI)
        private readonly DbServer _context;
        //private readonly SetMission _SetMission;
        private readonly AgentService _agentService;


        public AgentsController(DbServer context, AgentService agentService)
        {
            // SetMission setMission,
            _context = context;
            _agentService = agentService;
            //_SetMission = setMission;
        }


        //רשימת סוכנים - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var resolt = await _agentService.GetAllAgentsS();
            return Ok(resolt);
        }


        //יצירת סוכן - שרת סימולציה בלבד
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateAgents(Agent agent)
        {
            var resolt = _agentService.CrateAgentsS(agent);
            return Ok(resolt);
        }


        //הזזת מיקום סוכן - שרת סימולציה בלבד
        [HttpPut("{id}/move")]
        [Produces("application/json")]
        public async Task<IActionResult> MoveAgent(Guid id, string direction)
        {
            var resolt = _agentService.MoveAgentS(id, direction);
            return Ok(resolt);
        }
    }
}
