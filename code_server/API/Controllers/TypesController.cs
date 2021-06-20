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
    public class TypesController : ControllerBase
    {
        private readonly DataContext _context;

        public TypesController( DataContext context )
        {
            _context = context;
        }

        // Get All Types
        [HttpGet]
        public ActionResult<IEnumerable<UserType>> GetTypes()
        {
            var types = _context.Types.ToList();
            
            return types;
        }

        // Add Type
        [HttpPost]
        public StatusCodeResult InsertType()
        {
            UserType type = new TypeConstructor( Request.Form ).CreateUserTypeObjectFromData();
            
            if( type == null )
            {
                return StatusCode(500);
            }
            
            _context.Types.Add( type );
            
            int status = _context.SaveChanges();
            
            if( status == 0 )
            {
                return StatusCode(500);
            }
            
            return StatusCode(200);
        }
    }
}