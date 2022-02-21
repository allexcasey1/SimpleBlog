using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Application.Core;


namespace Application.BlogPosts
{
    public class Details
    {
        public class Query : IRequest<Result<BlogPost>>
        {
            public Guid Id { get; set; }  
        }

        public class Handler : IRequestHandler<Query, Result<BlogPost>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<BlogPost>> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogPost = await _context.BlogPosts
                    .Include(blog => blog.Content)
                    .ThenInclude(content => content.Sections)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                    
                if (blogPost == null) return Result<BlogPost>.Failure("Failed to load post.");

                return Result<BlogPost>.Success(blogPost);
            }
        }
    }
}