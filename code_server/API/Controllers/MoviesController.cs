using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using API.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers{
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
            if( id == null ){
                return null;
            }
            
            var movie = _context.Films.Find( id );
            
            return movie;
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
    }
}