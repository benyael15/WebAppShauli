using ShauliFinalProject.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliFinalProject.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JoinPostComments(string postText, string commentText)
        {
            if (string.IsNullOrWhiteSpace(postText) || string.IsNullOrWhiteSpace(commentText))
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                // Creates all possible combinations of post-comments pairs
                var results = (from post in db.Posts
                               join
                                   comment in db.Comments on
                                   post.Id equals comment.PostId

                               select new
                               {
                                   PostId = post.Id,
                                   PostHeadline = post.Headline,
                                   PostText = post.Content,
                                   CommentHeadline = comment.Headline,
                                   CommentText = comment.Content
                               });

                // Gets only the results of posts AND comments containing the text in headling or content
                results = results.Where(pc => ((pc.CommentHeadline.Contains(commentText)) || (pc.CommentText.Contains(commentText))) &&
                                    ((pc.PostText.Contains(postText)) || (pc.PostText.Contains(postText))));

                // Gets the posts that satisfied the conditions.
                List<Post> posts = db.Posts.Include(post => post.Comments).ToList();
                posts = posts.Where(post => results.Any(result => result.PostId == post.Id)).ToList();

                return View("../Blog/Index", posts);
            }
        }

        public ActionResult GetPostsByCategory(int category)
        {
            Category cat = (Category)category;

            var posts = db.Posts.ToList();

            // Gets the posts by category
            var postsByCategory = 
              from post in posts
              group post by post.Category into g
              select new { Category = g.Key, Posts = g.ToList() };

            // Gets only the posts of the requested category
            posts = postsByCategory.Where(c => c.Category == cat).First().Posts;

            return View("../Blog/Index", posts);
        }
    }
}