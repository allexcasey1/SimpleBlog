using System;
using System.Threading.Tasks;
using Application.BlogPosts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;

namespace Api.Controllers
{
    public class BlogPostsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<TitleList>>> GetBlogTitleList()
        {
            return HandleResult(await Mediator!.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(Guid id)
        {
            return HandleResult(await Mediator!.Send(new Details.Query { Id = id }));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(BlogPost blog)
        {
            return HandleResult(await Mediator!.Send(new Create.Command {Blog = blog}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(Guid id, BlogPost blog)
        {

            return HandleResult(await Mediator!.Send(new Update.Command(blog)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            return HandleResult(await Mediator!.Send(new Delete.Command {Id = id}));
        }
    }
}