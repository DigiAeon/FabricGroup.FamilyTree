using Microsoft.AspNetCore.Mvc;
using FabricGroup.FamilyTree.UI.Services.Interfaces;
using FabricGroup.FamilyTree.UI.Services.Interfaces.Models;

namespace FabricGroup.FamilyTree.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _service;

        public HomeController(IHomeControllerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(FindRelativeResponse))]
        [ProducesResponseType(404)]
        public IActionResult FindRelatives(FindRelativeRequest request)
        {
            var response = _service.FindRelatives(request);

            if (response == null || response.RelativeInfo == null || response.RelativeInfo.Count <= 0)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GetRelationshipsResponse))]        
        public IActionResult GetRelationships()
        {
            return Ok(_service.GetRelationships());
        }
    }
}
