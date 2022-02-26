using MediatR;
using Application.Models;
using Application.Core;
using Application.Builders;
using Persistence;
using Domain;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.BlogPosts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public BlogPostDto blogPost { get; set; } = new BlogPostDto();

        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.Users
                    .Include(u => u.Blog)
                    .ThenInclude(u => u!.BlogPosts)
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null || user.Blog == null) return Result<Unit>.Failure("Failed to create new blog post");

                request.blogPost.BlogId = user.Blog.Id;

                await _context.BlogPosts.AddAsync(_mapper.Map<BlogPost>(request.blogPost));

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create new blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}