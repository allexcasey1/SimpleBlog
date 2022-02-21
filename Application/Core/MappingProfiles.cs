using System.Linq;
using Domain;
using Application.BlogPosts;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<BlogPost, BlogPost>()
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content));
            CreateMap<BlogPost, TitleList>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title)); 
        }
    }
}