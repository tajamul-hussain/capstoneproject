using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOS;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,MemberDto>().
            ForMember(dest=>dest.PostUrl,opt=>opt.MapFrom(src=>src.Posts.FirstOrDefault(x=>x.IsMain).Url));
            CreateMap<Post,PostDto>();
            CreateMap<Comment,CommentDto>();
        }
    }
}