﻿using AutoMapper;
using Bloggin_platform.Dtos.Post;
using Bloggin_platform.Dtos.User;
using Bloggin_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Mapping
{
    public class DtoToModel : Profile
    {
        public DtoToModel()
        {
            CreateMap<UserInsertDto, User>();
            CreateMap<PostInsertDto, Post>();
        }
    }
}
