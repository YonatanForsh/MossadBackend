﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadBackend.DB;
using MossadBackend.Directions;
using MossadBackend.Models;
using MossadBackend.Tools;

namespace MossadBackend.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly DbServer _context;
        //private readonly SetMission _SetMission;
        private readonly TargetService _targetService;


        public TargetController(DbServer context, TargetService targetService)
        {
            // SetMission setMission,
            _context = context;
            //_SetMission = setMission;
            _targetService = targetService;
        }


        //רשימת מטרות - ללא מגבלת הרשאות
        [HttpGet]
        public async Task<IActionResult> GetAllTargets()
        {
            var resolt = await _targetService.GetAllTargetsS();
            return Ok(resolt);
        }

        //יצירת מטרה - שרת סימולציה בלבד
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrateTarget(Target target)
        {
            var resolt = _targetService.CrateTargetS(target);
            return Ok(resolt);
        }


        //הזזת מיקום מטרה - שרת סימולציה בלבד
        [HttpPut("{id}/move")]
        [Produces("application/json")]
        public async Task<IActionResult> MoveTarget(Guid id, string direction)
        {
            var resolt = _targetService.MoveTargetS(id, direction);
            return Ok(resolt);
        }
    }
}
