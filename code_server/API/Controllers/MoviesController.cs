using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        
        private readonly DataContext _context;
        
        public MoviesController( DataContext context )
        {
            _context = context;
        }
        
        // Get All Movies
        [HttpGet]
        public ActionResult<IEnumerable<AppFilm>> GetMovies()
        {
            var movies = _context.Films.ToList();
            
            return movies;
        }
        
        // Get Single Movie TODO Render Movie Clip Action
        [HttpGet("{id}")]
        public ActionResult<AppFilm> GetMovie( int id )
        {   
            if( id == 0 ){
                return null;
            }
            
            var movie = _context.Films.Find( id );
            
            return movie;
        } 
        
         [HttpGet("image/{id}")]
        public FileContentResult GetMovieImage( int id )
        {   
            if( id == 0 ){
                return null;
            }
            
            var movie = _context.Films.Find( id );
            byte[] imageArray = movie.PosterImage;
            
           return File(imageArray, "image/jpg");
        } 
        
        // Add Movie
        [HttpPost]
        public StatusCodeResult InsertMovie()
        {
            AppFilm film = new FilmConstructor( Request.Form ).CreateFilmObjectFromData();
            
            if( film == null )
            {
                return StatusCode(500);
            }
            _context.Films.Add( film );
            
            int status = _context.SaveChanges();
            
            if( status == 0 )
            {
                return StatusCode(500);
            }
            
            return StatusCode(200);
        }


        // Import Movies From Third Party API
        [HttpPost("import/{page}")]
        public StatusCodeResult ImportFilmsFromApi( int page )
        {      
            FilmImporter fm = new();
            string search_param = HttpContext.Request.Query["search"].ToString();
            
            string url = "http://www.omdbapi.com/?s=" + search_param + "&apikey=e8a33542&page=" + page.ToString();
            
            JToken films =  fm.LoadFilmsFromUrl( url );

            if ( films == null ){
                StatusCode( 500 );
            }

            for (int i = 0; i < 10; i++)
            {
                string imdb_id = films[i]["imdbID"].ToString();

                if( imdb_id != "" )
                {
                    JToken filmJson = fm.LoadFilmFromUrl( "http://www.omdbapi.com/?apikey=e8a33542&i=" + imdb_id );
                    AppFilm film = new FilmConstructor().CreateFilmObjectFromJSON( filmJson );

                    if( film == null )
                    {
                        continue;
                    } else {
                        _context.Films.Add( film );

                        int status = _context.SaveChanges();
            
                        if( status == 0 )
                        {
                            return StatusCode(500);
                        }
                    }
                } else {
                    continue;
                }
            }

            return StatusCode(200);
        }
    }
}