using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadBackend.DB;
using MossadBackend.Models;
using MossadBackend.ViewModel;

namespace MossadBackend.ViewControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewUniversalDetailsController : ControllerBase
    {
        public readonly DbServer _context;
        public readonly ViewUnivarsalDetails _viewUnivarsalDetails;

        public ViewUniversalDetailsController(DbServer context, ViewUnivarsalDetails viewUnivarsalDetails)
        {
            _context = context;
            _viewUnivarsalDetails = viewUnivarsalDetails;
        }

        [HttpGet]
        public IActionResult FillUniversalD()
        {
            int PassAgents = 0;
            int TargetCounts = _context.TargetesList.Count();
            int AgentCounts = _context.AgentsList.Count();
            int MissionCounts = _context.MissionsList.Count();
            foreach (Agent passAgent in _context.AgentsList)
            {
                if (passAgent.Status == "Passive")
                {
                    PassAgents += 1;
                }
                _viewUnivarsalDetails.AgentsCount = AgentCounts;
                _viewUnivarsalDetails.TargetsCount = TargetCounts;
                _viewUnivarsalDetails.MissionsCount = MissionCounts;
                _viewUnivarsalDetails.AgeTarRelation = AgentCounts / TargetCounts;
                _viewUnivarsalDetails.AgePasTarRelation = PassAgents / TargetCounts;
            }
            return Ok(_viewUnivarsalDetails);
        }
    }
}