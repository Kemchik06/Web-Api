using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesShopApi.Models;
using MoviesShopApi.Dtos;

namespace MoviesShopApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Movie, MovieDto>();

        
            CreateMap<Game, GameDto>();
            CreateMap<AudioBook, AudioBookDto>();


            // Dto to Domain
       
            CreateMap<MovieDto, Movie>()
              .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<GameDto, Game>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AudioBookDto, AudioBook>().ForMember(c => c.Id, opt => opt.Ignore());



        }

    }
}
