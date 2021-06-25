using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnnotationApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnnotationController : ControllerBase
    {
        private readonly IAnnotationService _annotationService;

        public AnnotationController(IAnnotationService annotationService)
        {
            _annotationService = annotationService;
        }

        [HttpGet]
        public ActionResult<List<IAnnotation>> Get()
        {
            return _annotationService.Get();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(Get))]
        public ActionResult<IAnnotation> Get(string id)
        {
            var annotation = _annotationService.Get(id);

            if (annotation == null)
            {
                return NotFound();
            }

            return (Annotation)annotation;
        }

        public ActionResult<List<IAnnotation>> GetByTarget(string id)
        {
            var annotations = _annotationService.GetByTarget(id);

            if (annotations == null)
            {
                return NotFound();
            }

            return annotations;
        }

        [HttpPost]
        public ActionResult<List<IAnnotation>> Search(Search search)
        {
            var annotations = _annotationService.Search(search.Text);

            if (annotations == null)
            {
                return NotFound();
            }

            return annotations;
        }

        [HttpPost]
        public ActionResult<IAnnotation> Create(IAnnotation annotation)
        {
            annotation.Created = DateTime.Now;
            _annotationService.Create(annotation);

            annotation.Id = "https://annotatorapi.azurewebsites.net/annotation/get/" + annotation.DbId;
            _annotationService.Update(annotation.DbId, annotation);

            //return CreatedAtRoute("Get", new { id = "https://annotatorapi.azurewebsites.net/annotation/get/" + annotation.DbId }, annotation);
            return Ok("https://annotatorapi.azurewebsites.net/annotation/get/" + annotation.DbId);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, IAnnotation annotationIn)
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

            _annotationService.Remove(annotation.DbId);

            return NoContent();
        }
    }
}
