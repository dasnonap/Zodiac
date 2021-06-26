using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController( DataContext context )
        {
            _context = context;
        } 

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser( int id )
        {
            if ( id == 0 )
            {
                return null;
            } 

            var user = _context.Users.Find( id );

            return user;
        }

        [HttpPost]
        public StatusCodeResult AddUser()
        {   
            int type_id = Int32.Parse( Request.Form["type"] );
            
            AppUser user = new UserConstructor( Request.Form ).CreateUserObjectFromData( type_id  );
            
            if( user == null )
            {
                return StatusCode( 500 );
            }
            
            _context.Users.Add( user );
            
            int status = _context.SaveChanges();
            
            if( status == 0 )
            {
                return StatusCode(500);
            }
            
            return StatusCode(200);
        }        
    }
}