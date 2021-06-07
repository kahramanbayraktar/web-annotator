using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnnotationApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnnotationController : ControllerBase
    {
        private readonly AnnotationService _annotationService;

        public AnnotationController(AnnotationService annotationService)
        {
            _annotationService = annotationService;
        }

        [HttpGet]
        public ActionResult<List<Annotation>> Get()
        {
            return _annotationService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Annotation> Get(string id)
        {
            var annotation = _annotationService.Get(id);

            if (annotation == null)
            {
                return NotFound();
            }

            return annotation;
        }

        [HttpGet("{id}")]
        public ActionResult<List<Annotation>> GetByTarget(string id)
        {
            var annotations = _annotationService.GetByTarget(id);

            if (annotations == null)
            {
                return NotFound();
            }

            return annotations;
        }

        [HttpPost]
        public ActionResult<Annotation> Create(Annotation annotation)
        {
            _annotationService.Create(annotation);

            return CreatedAtRoute("GetAnnotation", new { id = annotation.DbId }, annotation);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Annotation annotationIn)
        {
            var annotation = _annotationService.Get(id);

            if (annotation == null)
            {
                return NotFound();
            }

            _annotationService.Update(id, annotationIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var annotation = _annotationService.Get(id);

            if (annotation == null)
            {
                return NotFound();
            }

            _annotationService.Remove(annotation.Id);

            return NoContent();
        }
    }
}
