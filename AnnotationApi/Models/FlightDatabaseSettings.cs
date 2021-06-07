namespace AnnotationApi.Models
{
    public class FlightDatabaseSettings : IFlightDatabaseSettings
    {
        public string RoutesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFlightDatabaseSettings
    {
        string RoutesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
