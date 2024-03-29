using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


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

        [HttpPost("login")]
        public IActionResult Login()
        {   
            AppUser user = new AppUser();
            
            user.UserName = Request.Form["username"];
            user.Password = Request.Form["password"];
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var users = _context.Users.ToList();
            AppUser logged = new AppUser();
            foreach( AppUser _user in users ){
                if( user.UserName == _user.UserName ){
                    if( user.Password == _user.Password ){
                        logged = user;
                    }
                }
            }

            if ( logged != null )
            {
                var claims = new[] {    
                    new Claim("username", logged.UserName ),    
                    new Claim("id", logged.AppUserId.ToString() )   
                };  
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:4223",
                    audience: "http://localhost:4200",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost("register")]
        public IActionResult Register()
        {   int type_id  = 1;
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
            
           
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:4223",
                audience: "http://localhost:4200",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });

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