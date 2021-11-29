using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ClinicManagement.Web.DTO;
using ClinicManagement.Web.Models;

namespace ClinicManagement.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<City, CityDto>();

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientDto>());
            //var mapper = new Mapper(config);


        }
    }

    //public static class AutoMapperService
    //{
    //    public static MapperConfiguration Initialize()
    //    {
    //        MapperConfiguration config = new MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile<MappingProfile>();
    //        });

    //        return config;
    //    }
    //}
}