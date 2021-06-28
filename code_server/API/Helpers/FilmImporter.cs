using System.Collections.Generic;
using System.Linq;
using System.Net;
using API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Helpers
{
    public class FilmImporter
    {
        public JToken LoadFilmsFromUrl( string url )
        {
            WebClient client = new WebClient();
           
            string downloadString = client.DownloadString( url );
           
            JObject jsonArray = JObject.Parse( downloadString );


            return jsonArray["Search"];
        }

        public JToken LoadFilmFromUrl( string url )
        {
            WebClient client = new WebClient();

            string downloadString = client.DownloadString( url );

            JObject jsonArray = JObject.Parse( downloadString );

            return jsonArray;

        }
    }
}