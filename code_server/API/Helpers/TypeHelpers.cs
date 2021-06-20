using API.Entities;
using API.Validators;
using Microsoft.AspNetCore.Http;

using System;
namespace API.Helpers
{
    public class TypeConstructor
    {
        private IFormCollection formData;

        public TypeConstructor( IFormCollection data )
        {
            formData = data;
        } 

        public UserType CreateUserTypeObjectFromData()
        {   
            UserType type = new UserType();

            string name = formData["name"];
            float price = float.Parse( formData["price"] );

            if( new Validator( name ).IsValidField() )
            {
                type.Name = name;
            } 
            else
            {
                return null;
            }
            
            if( new Validator( price ).IsValidFloat() )
            {
                type.Price = price;
            } 
            else
            {
                return null;
            }

            return type;
        }
    }
}