
using Application.Core;
using MediatR;
using Persistence;

namespace Application.BlogPosts
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id {get; set; }
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
                var blogPost = await _context.BlogPosts.FindAsync(request.Id);

                if (blogPost == null) return null!;

                _context.BlogPosts.Remove(blogPost);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}