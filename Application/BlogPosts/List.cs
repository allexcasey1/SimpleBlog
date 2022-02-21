using Microsoft.AspNetCore.Html;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Core;
using Application.Builders;
using Domain;

namespace Application.BlogPosts
{
    public class List
    {
        public class Query : IRequest<Result<List<TitleList>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<TitleList>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<TitleList>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogTitles = await _context.BlogPosts
                    .ProjectTo<TitleList>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<TitleList>>.Success(blogTitles);
            }
        }
    }
}