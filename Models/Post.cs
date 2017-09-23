using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliFinalProject.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Author { get; set; }
        public string Website { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public int CompareTo(Post other)
        {
            return (DateTime.Compare(other.Timestamp, this.Timestamp));
        }
    }

    public enum Category
    {
        Sport,
        Blog,
        News,
        Entertainment
    }
}