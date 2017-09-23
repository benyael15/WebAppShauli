using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliFinalProject.Models
{
    public class BlogUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}