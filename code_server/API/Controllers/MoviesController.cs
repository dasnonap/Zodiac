using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System;
using API.Repositories;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;
        private FilmRepo _films;
        public MoviesController( DataContext context )
        {
            _context = context;
            _films = new FilmRepo( _context );
        }
        
        // Get All Movies
        [HttpGet,Authorize]
        public IEnumerable<AppFilm> GetMovies()
        {
            return _films.GetRepoFilms();
        }
        
        // Get Single Movie TODO Render Movie Clip Action
        [HttpGet("{id}"),Authorize]
        public ActionResult<AppFilm> GetMovie( int id )
        {   
            if( id == 0 ){
                return null;
            }
            
            return _films.GetRepoFilm( id );
        } 

        [HttpGet("movie")]
        public IActionResult  Stream(string id) {
            var path = @"D:\Университет\4 курс\Дипломна работа\videos\test.mp4";
            var res = File(System.IO.File.OpenRead(path), "video/mp4");
            res.EnableRangeProcessing = true;
            return res;
        }

        // Get Movie per Pages
        [HttpGet("listing"),Authorize]
        public IEnumerable<AppFilm> GetMoviePages( int page_id )
        {  
            return _films.GetFilmsListing( page_id );
        } 

        // Search Movies
        [HttpGet("search"),Authorize]
        public IEnumerable<AppFilm> GetSearchResultPages( string s )
        {  
            
            return _films.GetSearchResults( s );
        } 

        [HttpGet("category"),Authorize]
        public IEnumerable<AppFilm> GetCategoryResultPages( string c )
        {  
            
            return _films.GetCategoryResults( c );
        } 
        
         [HttpGet("image/{id}")]
        public ActionResult GetMovieImage( int id, int width, int height)
        {   
            Image image;
            if( id == 0 ){
                return null;
            }

            if ( width == 0 && height == 0 ){
                return null;
            }
            
            var movie = _films.GetImageFilm( id );
            byte[] imageArray = movie.PosterImage;

            using (MemoryStream mStream = new MemoryStream(imageArray))
            {
                image = Image.FromStream(mStream);
            }

            Bitmap b = new Bitmap(image); 

            Image resized_image = new FilmConstructor().resizeImage(b, new Size(width, height));

            MemoryStream ms = new MemoryStream();

            resized_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/png");
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