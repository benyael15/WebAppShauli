using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliFinalProject.Models
{
    public class Fan
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Type { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Seniority { get; set; }

    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}