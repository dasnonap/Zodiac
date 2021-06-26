using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using API.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly DataContext _context;

        public SeriesController( DataContext context )
        {
            _context = context;
        }

        // Get All Series
        [HttpGet]
        public ActionResult<IEnumerable<AppSeries>> GetSeries()
        {
            var series = _context.Series.ToList();
            
            return series;
        }


        // Get Single Movie TODO Render Movie Clip Action
        [HttpGet("{id}")]
        public ActionResult<AppSeries> GetSeries( int id )
        {   
            if( id == 0 ){
                return null;
            }
            
            var series = _context.Series.Find( id );
            
            return series;
        }  


        // Add Series
        [HttpPost]
        public StatusCodeResult InsertSeries()
        {
            AppSeries series = new SeriesConstructor( Request.Form ).CreateSeriesObjectFromData();
            
            if( series == null )
            {
                return StatusCode(500);
            }
            _context.Series.Add( series );
            
            int status = _context.SaveChanges();
            
            if( status == 0 )
            {
                return StatusCode(500);
            }
            
            return StatusCode(200);
        }

    }
        
}