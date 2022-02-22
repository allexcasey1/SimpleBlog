
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var blog = await _context.BlogPosts
                    .FirstOrDefaultAsync(x => x.Id == request.Blog.Id);

                if (blog == null) return null!;

                var sections = await _context.Sections
                    .Where(x => EF.Property<Guid>(x, "ContentId") == request.Blog.Content.Id)
                    .ToListAsync();

                _context.Sections.RemoveRange(sections);

                request.Blog.Content.Sections.ForEach(section => {
                    _context.Sections.Add(section);
                });

                _mapper.Map(request.Blog, blog);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}