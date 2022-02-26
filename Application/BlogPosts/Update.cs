/// Update blogPost
/// Supports simple update where users can add/remove from end of list

using Application.Core;
using Application.Models;
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
            public BlogPostDto BlogPost { get; set; }

            public Command(BlogPostDto blogPost) 
            {
                BlogPost = blogPost;
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
                if (request.BlogPost == null) return Result<Unit>.Failure("Invalid request"); 

                var blogPost = await _context.BlogPosts
                    .Include(x => x.Content)
                    .ThenInclude(x => x.Sections)
                    .FirstOrDefaultAsync(x => x.Id == request.BlogPost.Id);

                if (blogPost == null) return Result<Unit>.Failure("Invalid blog post");

                request.BlogPost.Content.Sections.ForEach(section => section.ContentId = blogPost.Content.Id);

                // get count of db entity, request, and intersection of each
                var currentSectionsCount = blogPost.Content.Sections.Count;
                var requestSectionsCount = request.BlogPost.Content.Sections.Count;
                var sameSectionsCount = currentSectionsCount < requestSectionsCount ?
                    currentSectionsCount : requestSectionsCount;

                // check if adding or removing any sections
                var isAdding = requestSectionsCount > currentSectionsCount;
                var isSubtracting = requestSectionsCount < currentSectionsCount;
                
                // modify db entity where sections are the same
                for (int i = 0; i < sameSectionsCount; i++)
                {
                    blogPost.Content.Sections[i] = _mapper.Map<Section>(request.BlogPost.Content.Sections[i]);
                }  

                // handle added sections
                if (isAdding) 
                {
                    var addedSections = request.BlogPost.Content.Sections
                            .GetRange(currentSectionsCount, requestSectionsCount - currentSectionsCount);

                    addedSections.ForEach(section => 
                    {
                        _context.Sections.Add(_mapper.Map<Section>(section));
                    });
                }

                // handle removed sections
                if (isSubtracting)
                {
                    var sectionsToDelete = currentSectionsCount - requestSectionsCount;

                    Console.WriteLine($"{sectionsToDelete}, {currentSectionsCount}, {requestSectionsCount}");

                    blogPost.Content.Sections.RemoveRange(currentSectionsCount - sectionsToDelete, sectionsToDelete);
                }

                blogPost.Title = request.BlogPost.Title;

                var result = _context.SaveChanges() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update blog post");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}