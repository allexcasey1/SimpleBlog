using Persistence;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security
{
    public class IsAuthorRequirement : IAuthorizationRequirement
    {}

    public class IsAuthorRequirementHandler : AuthorizationHandler<IsAuthorRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsAuthorRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsAuthorRequirement requirement)
        {
            // lambda to get blog id dynamically from http context
            // var x = Lambda<Func<KeyValuePair<string, Guid>, bool>>.Cast;
            // var getId = x(x => x.Key == "id");

            var userId = context.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) 
                return Task.CompletedTask;

            var blogPostId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString()!);

            var user = _dbContext.Users
                .Include(u => u.Blog)
                .ThenInclude(u => u.BlogPosts)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == userId);

            if (user == null) 
                return Task.CompletedTask;

            var blogPost = user.Blog!.BlogPosts.Where(x => x.Id == blogPostId);

            if (blogPost != null) context
                .Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public sealed class Lambda<T>
    {
        public static Func<T, T> Cast = x => x;        
    }
}