using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class FilmRepo
    {
        public List<AppFilm> _films = new List<AppFilm>();
        private readonly DataContext _context;

        public FilmRepo( DataContext context )
        {   
            _context = context;
            UpdateRepoFilms();
        }

        public List<AppFilm> GetRepoFilms(){

            if( _films != null ){
                return _films;
            }

            List<AppFilm> films = _films;
            foreach( AppFilm film in films ){
                film.PosterImage = Encoding.ASCII.GetBytes( "https://localhost:4223/api/movies/image/" + film.AppFilmId ) ;
            }


            return films;
        }

        private void UpdateRepoFilms(){

            _films = _context.Films.ToList();
            
        }

        public AppFilm GetRepoFilm( int id ){
            foreach( AppFilm film in _films ){
                if( film.AppFilmId == id ){
                    AppFilm result = film;
                    result.PosterImage = Encoding.ASCII.GetBytes( "https://localhost:4223/api/movies/image/" + result.AppFilmId ) ;

                    return result;
                }
               
            }

            return null;
        }

        public AppFilm GetImageFilm( int id ){
            foreach( AppFilm film in _films ){
                if( film.AppFilmId == id ){
                    AppFilm result = film;

                    return result;
                }
               
            }

            return null;
        }

        public List<AppFilm> GetFilmsListing( int page_id )
        {
            int total_number =  _films.Count;
            int page_size = 10;

            if( total_number / page_size < page_id ){
                return null;
            }

            List<AppFilm> films = _films.Skip( ( page_id - 1 ) * page_size ).Take(page_size).ToList();

            foreach( AppFilm film in films ){
                film.PosterImage = Encoding.ASCII.GetBytes( "https://localhost:4223/api/movies/image/" + film.AppFilmId ) ;
            }

            return films;
        }

        public List<AppFilm> GetSearchResults( string search_param ){
            
            if( search_param == null ){
                return null;
            }
            search_param = search_param.ToLower();

            List<AppFilm> result = new List<AppFilm>();

            foreach( AppFilm film in _films ){
                AppFilm film_result = null;
                
                
                if( film.Name.ToLower().Contains( search_param ) ){
                   film_result = film;
                } else if( film.Description.ToLower().Contains( search_param ) ){
                    film_result = film;
                } else if ( film.Actors.ToLower().Contains( search_param ) ){
                    film_result = film;
                }  else if ( film.Genres.ToLower().Contains( search_param ) ){
                    film_result = film;
                } else if ( film.Year.ToLower().Contains( search_param ) ){
                    film_result = film;
                } else {
                    film_result = null;
                }

                if( film_result != null ){
                    film_result.PosterImage = Encoding.ASCII.GetBytes( "https://localhost:4223/api/movies/image/" + film_result.AppFilmId ) ;

                    result.Add( film_result );
                }
            }

            return result;
        }

        public List<AppFilm> GetCategoryResults( string category ){
            
            if( category == null ){
                return null;
            }
            category = category.ToLower();

            List<AppFilm> result = new List<AppFilm>();

            foreach( AppFilm film in _films ){
                AppFilm film_result = null;
                
                
                if( film.Genres.ToLower().Contains( category ) ){
                   film_result = film;
                } else {
                    film_result = null;
                }

                if( film_result != null ){
                    film_result.PosterImage = Encoding.ASCII.GetBytes( "https://localhost:4223/api/movies/image/" + film_result.AppFilmId ) ;

                    result.Add( film_result );
                }
            }

            return result;
        }
    }
}