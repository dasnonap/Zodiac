using API.Entities;
using API.Validators;
using Microsoft.AspNetCore.Http;

using System;
namespace API.Helpers
{
    public class UserConstructor
    {
        private IFormCollection formData;

        public UserConstructor( IFormCollection data )
        {
            formData = data;
        }

        public AppUser CreateUserObjectFromData( int typeId )
        {
            AppUser user = new AppUser();

            string username = formData["username"];
            string firstName = formData["first-name"];
            string lastName = formData["last-name"];
            string password = formData["password"];
            string iban = formData["iban"];
            string email = formData["email"];

            if( new Validator( username ).IsValidField() )
            {
                user.UserName = username;
            } 
            else
            {
                return null;
            }

            if( new Validator( firstName ).IsValidField() )
            {
                user.FirstName = firstName;
            } 
            else
            {
                return null;
            }

            if( new Validator( lastName ).IsValidField() )
            {
                user.LastName = lastName;
            } 
            else
            {
                return null;
            }

            if( new Validator( password ).IsValidField() )
            {
                user.Password = password;
            } 
            else
            {
                return null;
            }

            if( new Validator( iban ).IsValidField() )
            {
                user.Iban = iban;
            } 
            else
            {
                return null;
            }

            if( new Validator( email ).IsValidEmail() )
            {
                user.Email = email;
            } 
            else
            {
                return null;
            }

           user.UserTypeId = typeId;
    
           return user;
        }

    }
}