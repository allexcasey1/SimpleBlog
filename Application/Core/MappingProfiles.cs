using System.Linq;
using Domain;
using Application.BlogPosts;
using Application.Models;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<Content, ContentDto>();
            CreateMap<Section, SectionDto>();

            CreateMap<UserDto, User>();
            CreateMap<BlogDto, Blog>();
            CreateMap<BlogPostDto, BlogPost>()
                .ForMember(d => d.BlogId, opt => {opt.UseDestinationValue(); opt.Ignore();});
            CreateMap<ContentDto, Content>()
                // .ForMember(d => d.Sections, opt => opt.NullSubstitute(new Section()))
                .ForMember(d => d.BlogPostId, opt => 
                {
                    opt.PreCondition(src => src.BlogPostId == null);
                    opt.UseDestinationValue(); opt.Ignore();
                });
            CreateMap<SectionDto, Section>();
                // .ForMember(d => d.ContentId, opt => 
                // {
                //     opt.PreCondition(src => src.ContentId == null);
                //     opt.UseDestinationValue(); opt.Ignore();
                // });

            CreateMap<BlogPost, TitleList>();
        }
    }
}