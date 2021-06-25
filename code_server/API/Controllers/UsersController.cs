using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            AppUser user = new UserConstructor( Request.Form ).CreateUserObjectFromData();

            )
        }        
    }
}