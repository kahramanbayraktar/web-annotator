using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnnotationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public ActionResult<List<Route>> Get()
        {
            return _routeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetRoute")]
        public ActionResult<Route> Get(string id)
        {
            var route = _routeService.Get(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        [HttpPost]
        public ActionResult<Route> Create(Route route)
        {
            _routeService.Create(route);

            return CreatedAtRoute("GetRoute", new { id = route.Id.ToString() }, route);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Route routeIn)
        {
            var route = _routeService.Get(id);

            if (route == null)
            {
                return NotFound();
            }

            _routeService.Update(id, routeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var route = _routeService.Get(id);

            if (route == null)
            {
                return NotFound();
            }

            _routeService.Remove(route.Id);

            return NoContent();
        }
    }
}
