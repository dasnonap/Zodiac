using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AppSeries
    {
        public int AppSeriesId { get; set; }

        public string Name { get; set; }

        public string Actors { get; set; }

        public string Year { get; set; }
        
        public int Season { get; set; }
        
        public int Episode { get; set; }

        public string Genres { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        
    }
}