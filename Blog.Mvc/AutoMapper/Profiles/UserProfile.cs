using AutoMapper;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;

namespace Blog.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
        }
    }
}
