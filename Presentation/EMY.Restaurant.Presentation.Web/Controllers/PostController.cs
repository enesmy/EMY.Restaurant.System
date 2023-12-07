using EMY.Restaurant.Core.Application.Abstract;
using EMY.Restaurant.Core.Domain.Entities;
using EMY.Restaurant.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMY.Restaurant.Presentation.Web.Controllers
{
    public class PostController : Controller
    {

        private readonly IDatabaseFactory Database;
        public PostController(IDatabaseFactory database)
        {
            Database = database;
        }

        public IActionResult Index()
        {
          
            return View();
        }

        public async Task<IActionResult> SavePost(Post post)
        {
            if (post == null)
            {
                return BadRequest("Post is empty!");
            }
            if (string.IsNullOrEmpty(post.Caption))
            {
                return BadRequest("Caption is empty!");
            }
            if (string.IsNullOrEmpty(post.Content))
            {
                return BadRequest("Content is empty!");
            }

            if(post.PostID==Guid.Empty)
            {

            }

            return Ok(post);
        }
    }
}
