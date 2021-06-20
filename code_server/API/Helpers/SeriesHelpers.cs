using API.Entities;
using API.Validators;
using Microsoft.AspNetCore.Http;
using System;

namespace API.Helpers
{
    public class SeriesConstructor
    {
        private IFormCollection formData;

        public SeriesConstructor( IFormCollection data )
        {
            formData = data;
        }

        public AppSeries CreateSeriesObjectFromData()
        {
            AppSeries series = new AppSeries();

            string name = formData["name"];
            string description = formData["description"];
            string year = formData["year"];
            string actors = formData["actors"];
            int season = Int32.Parse( formData["season"] );
            int episode = Int32.Parse( formData["episode"] );
            string genres = formData["genres"];
            string location = "none";


            if( new Validator( name ).IsValidField() )
            {
                series.Name = name;
            } 
            else
            {
                return null;
            } 

            if( new Validator( description ).IsValidField() )
            {
                series.Description = description;
            } 
            else
            {
                return null;
            } 

            if( new Validator( year ).IsValidField() )
            {
                series.Year = year;
            } 
            else
            {
                return null;
            } 

            if( new Validator( actors ).IsValidField() )
            {
                series.Actors = actors;
            } 
            else
            {
                return null;
            } 

            if( new Validator( season ).IsValidInteger() )
            {
                series.Season = season;
            } 
            else
            {
                return null;
            } 

            if( new Validator( episode ).IsValidInteger() )
            {
                series.Episode = episode;
            } 
            else
            {
                return null;
            } 

            if( new Validator( genres ).IsValidField() )
            {
                series.Genres = genres;
            } 
            else
            {
                return null;
            } 

            series.Location = location;
            
            return series;
        }
    }
}