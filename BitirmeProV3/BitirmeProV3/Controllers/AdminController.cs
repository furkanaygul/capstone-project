﻿using BitirmeProV3.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitirmeProV3.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        // GET: Account

        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}