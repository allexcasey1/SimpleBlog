
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.BlogPosts 
{
    public class Update
    {
        public class Command : IRequest<Result<Unit>>
        {
            public BlogPost Blog { get; set; } = new BlogPost();

            public Command(BlogPost blogPost) 
            {
                Blog = blogPost;
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private DataContext _context;
            private IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var blog = await _context.BlogPosts.FindAsync(request.Blog.Id);

                if (blog == null) return null!;

                while (blog.Content.Sections.Count > request.Blog.Content.Sections.Count)
                {
                    blog.Content.Sections.RemoveAt(blog.Content.Sections.Count - 1);
                }

                _mapper.Map(request.Blog, blog);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}