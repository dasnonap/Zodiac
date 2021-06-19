using API.Entities;
using API.Validators;
using Microsoft.AspNetCore.Http;

namespace API.Helpers
{
    public class FilmConstructor
    {
        private IFormCollection formData;
        
        public FilmConstructor( IFormCollection data )
        {
            formData = data;
        }
        
        // Create AppFilm Object From Form Data input
        public AppFilm CreateFilmObjectFromData()
        {   
            AppFilm film = new AppFilm();
            
            string name = formData["name"];
            string actors = formData["actors"];
            string desc = formData["description"];
            string year = formData["year"];
            string genres = formData["genres"];
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
            
            film.Location = location;
            
            return film;
        }
    }
}