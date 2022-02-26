using System;
using System.Threading.Tasks;
using Application.BlogPosts;
using Application.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    public class BlogPostsController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<TitleList>>> GetBlogTitleList()
        {
            return HandleResult(await Mediator!.Send(new List.Query()));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(Guid id)
        {
            return HandleResult(await Mediator!.Send(new Details.Query { Id = id }));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(BlogPostDto blog)
        {
            return HandleResult(await Mediator!.Send(new Create.Command {blogPost = blog}));
        }

        [Authorize(Policy = "IsBlogAuthor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(Guid id, BlogPostDto blog)
        {
            blog.Id = id;
            return HandleResult(await Mediator!.Send(new Update.Command(blog)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            return HandleResult(await Mediator!.Send(new Delete.Command {Id = id}));
        }
    }
}