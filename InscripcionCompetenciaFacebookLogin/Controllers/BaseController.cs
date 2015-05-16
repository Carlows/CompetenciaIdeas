using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using InscripcionCompetenciaFacebookLogin.Models;

namespace InscripcionCompetenciaFacebookLogin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        protected ApplicationUserManager _userManager;
        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

    }
}