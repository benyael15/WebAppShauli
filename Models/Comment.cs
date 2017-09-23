using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliFinalProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Headline { get; set; }
        public string Author { get; set; }
        public string Website { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
    }
}