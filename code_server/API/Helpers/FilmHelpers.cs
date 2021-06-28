using System.Net;
using API.Entities;
using API.Validators;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace API.Helpers
{
    public class FilmConstructor
    {
        private IFormCollection formData;
        
        public FilmConstructor( IFormCollection data )
        {
            formData = data;
        }

        public FilmConstructor(){}
        
        // Create AppFilm Object From Form Data input
        public AppFilm CreateFilmObjectFromData()
        {   
            AppFilm film = new AppFilm();
            
            string name = formData["name"];
            string desc = formData["description"];
            string year = formData["year"];
            string genres = formData["genres"];
            string actors = formData["actors"];
            string poster_url = formData["posterImage"];
            string location = "none";
            
            if( new Validator( name ).IsValidField() )
            {
                film.Name = name;
            } 
            else
            {
                return null;
            }
            
            if( new Validator( actors ).IsValidField() )
            {
                film.Actors = actors;
            }
            else
            {
                return null;
            }
            
            if( new Validator( desc ).IsValidField() )
            {
                film.Description = desc;
            }
            else
            {
                return null;
            }
            
            if( new Validator( year ).IsValidField() )
            {
                film.Year = year;
            }
            else
            {
                return null;
            }
            
            if( new Validator( genres ).IsValidField() )
            {
                film.Genres = genres;
            }
            else
            {
                return null;
            }
            
            if( new Validator( poster_url ).IsValidField() )
            {   
                WebClient webClient = new WebClient();
                if( poster_url == null )
                {
                    return null;
                }
                byte[] imageBytes = webClient.DownloadData(poster_url);
                
                film.PosterImage  = imageBytes;
            }
            
            film.Location = location;
            
            return film;
        }

        public AppFilm CreateFilmObjectFromJSON( JToken token ) 
        {   
            AppFilm film = new AppFilm();
            
            string _name = token["Title"].ToString();
            string _desc = token["Plot"].ToString();
            string _year = token["Year"].ToString();
            string _genres = token["Genre"].ToString();
            string _actors = token["Actors"].ToString();
            string poster_url = token["Poster"].ToString();
            string location = "test location";
            
            if( new Validator( _name ).IsValidField() )
            {
                film.Name = _name;
            } 
            else
            {
                return null;
            }
            
            if( new Validator( _actors ).IsValidField() )
            {
                film.Actors = _actors;
            }
            else
            {
                return null;
            }
            
            if( new Validator( _desc ).IsValidField() )
            {
                film.Description = _desc;
            }
            else
            {
                return null;
            }
            
            if( new Validator( _year ).IsValidField() )
            {
                film.Year = _year;
            }
            else
            {
                return null;
            }
            
            if( new Validator( _genres ).IsValidField() )
            {
                film.Genres = _genres;
            }
            else
            {
                return null;
            }
            
            if( new Validator( poster_url ).IsValidField() )
            {   
                WebClient webClient = new WebClient();
                poster_url = poster_url.Replace( "_SX300", "" );
                if( poster_url == null )
                {
                    return null;
                }
                byte[] imageBytes = webClient.DownloadData(poster_url);
                
                film.PosterImage  = imageBytes;
            }
            
            film.Location = location;
            
            return film;
        }
    }
}