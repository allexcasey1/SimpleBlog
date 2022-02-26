using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Models;

namespace Application.BlogPosts
{
    public class Details
    {
        public class Query : IRequest<Result<BlogPostDto>>
        {
            public Guid Id { get; set; }  
        }

        public class Handler : IRequestHandler<Query, Result<BlogPostDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<BlogPostDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogPost = await _context.BlogPosts
                    .Include(x => x!.Content)
                    .ThenInclude(y => y!.Sections)
                    .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                    
                if (blogPost == null) return Result<BlogPostDto>.Failure("Failed to load post.");

                return Result<BlogPostDto>.Success(blogPost);
            }
        }
    }
}