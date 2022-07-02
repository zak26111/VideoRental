using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoRental.Dtos;
using VideoRental.Models;

namespace VideoRental.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Model - Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            //Dto - Model
            Mapper.CreateMap<CustomerDto, Customer>();           
            Mapper.CreateMap<MemberShipType, MemberShipTypeDto>();           
            Mapper.CreateMap<MemberShipTypeDto, MemberShipType>();

            //Model - Dto
            Mapper.CreateMap<Movie, MovieDto>();
            //Dto - Model
            Mapper.CreateMap<MovieDto, Movie>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>();

        }
    }
}