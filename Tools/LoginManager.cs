using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShauliFinalProject.Models;

namespace ShauliFinalProject.Tools
{
    public class LoginManager
    {
        private static LoginManager _instance;

        public static LoginManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoginManager();

                return _instance;
            }
        }

        public BlogUser CurrentLogedUser { get; set; }
    }
}