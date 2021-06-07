using AnnotationApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AnnotationApi.Services
{
    public class RouteService
    {
        private readonly IMongoCollection<Route> _routes;

        public RouteService(IFlightDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _routes = database.GetCollection<Route>(settings.RoutesCollectionName);
        }

        //public List<Route> Get() =>
        //    _routes.Find(route => true).ToList();

        public List<Route> Get()
        {
            List<Route> routes;
            routes = _routes.Find(r => true).Limit(20).ToList();
            return routes;
        }

        public Route Get(string id) =>
            _routes.Find<Route>(route => route.Id == id).FirstOrDefault();

        public Route Create(Route route)
        {
            _routes.InsertOne(route);
            return route;
        }

        public void Update(string id, Route routeIn) =>
            _routes.ReplaceOne(route => route.Id == id, routeIn);

        public void Remove(Route routeIn) =>
            _routes.DeleteOne(route => route.Id == routeIn.Id);

        public void Remove(string id) =>
            _routes.DeleteOne(route => route.Id == id);
    }
}
