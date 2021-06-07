using JsonLD.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace AnnotationApi.Client.Tests
{
    [TestClass]
    public class JsonLdConverterTests
    {
        //[TestMethod]
        //public void SimpleProductTest()
        //{
        //    var product = new Product()
        //    {
        //        Name = "T3 REPLICA NISSAN ALTIMA, MAXIMA (PAINTED/SILVER)",
        //    };
        //    Review review1 = new Review() { Name = "Review1", ReviewRating = new Rating() { RatingValue = "5" }, ReviewBody = "Best product ever!", Author = new Person() { Name = "Some Guy" } };
        //    Review review2 = new Review() { Name = "Review2", ReviewRating = new Rating() { RatingValue = "4" }, ReviewBody = "I've seen better...", Author = new Person() { Name = "Other Guy" } };
        //    product.Reviews = new List<Review> { review1, review2 };

        //    var jsonLd = product.ToJson();
        //}

        //[TestMethod]
        //public void ConvertAnnotationToJsonLd()
        //{
        //    var anno = new Annotation
        //    {
        //        Context = "http://www.w3.org/ns/anno.jsonld",
        //        Body = "Some annotation",
        //        Target = new Target
        //        {
        //            Id = "some url",
        //            Format = "image/png",
        //            Type = "Image"
        //        },
        //        Type = "Annotation"
        //    };

        //    var jsonLd = anno.ToString();
        //}
        
        [TestMethod]
        public void JsonLdEntities()
        {
            var anno = new Models.Annotation
            {
                Context = "http://www.w3.org/ns/anno.jsonld",
                Body = "Some annotation",
                Target = new Models.Target
                {
                    Id = "some url",
                    Format = "image/png",
                    Type = "Image"
                },
                Type = "Annotation"
            };
            //var @context = JObject.Parse("{ '@context': 'http://www.w3.org/ns/anno.jsonld' }");
            var contextProvider = new StaticContextProvider();
            //contextProvider.SetContext(typeof(Annotation), @context);

            // when
            IEntitySerializer serializer = new EntitySerializer(contextProvider);
            dynamic json = serializer.Serialize(anno);
            var str = json.ToString();
        }
    }
}
