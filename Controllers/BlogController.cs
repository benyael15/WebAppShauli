using ShauliFinalProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ShauliFinalProject.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            var posts = db.Posts.Include(post => post.Comments).ToList();

            //posts.Sort();

            return View(posts);
        }
    }
}