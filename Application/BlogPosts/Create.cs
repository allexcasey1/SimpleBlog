using MediatR;
using Domain;
using Application.Core;
using Application.Builders;
using Persistence;

namespace Application.BlogPosts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            // public BlogDetails Details { get; set; } = new BlogDetails();
            // public List<Section> Sections { get; set; } = new List<Section>();
            public BlogPost Blog { get; set; } = new BlogPost();

        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var blogPost = request.Blog;

                foreach(var section in blogPost.Content.Sections)
                {
                    _context.Sections.Add(section);
                }
                
                _context.Contents.Add(blogPost.Content);

                _context.BlogPosts.Add(blogPost);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create new blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}