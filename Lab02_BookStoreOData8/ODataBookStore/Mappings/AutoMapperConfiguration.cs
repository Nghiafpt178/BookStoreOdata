﻿using AutoMapper;
using Microsoft.Win32;
using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Book, BookRespond>().ReverseMap();

        }
    }
}
