﻿using Humanizer;
using HW_EF.Models;
using HW_EF.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Diagnostics;

namespace HW_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext blogDbContext;

        public HomeController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public IActionResult Index()
        {
            return View(blogDbContext.Posts.Include(x => x.PostsTags)
                                           .ThenInclude(t => t.Tag)
                                           .Include(x => x.Category));
        }

        [HttpGet]
        public IActionResult Search(string searchStr) //TODO: сделать так, чтобы поиск выполнялся по набору букв, а не по идентичному названию
        {
            if(!string.IsNullOrWhiteSpace(searchStr))
            {
                var posts = blogDbContext.Posts.Include(x => x.PostsTags)
                                           .ThenInclude(t => t.Tag)
                                           .Include(x => x.Category)
                                           .Where(x => x.Category == searchStr.Select(s => s.ToString()))
                                           .Where(t => t.Title == searchStr);

                //var request = blogDbContext.Posts.Where(t => t.Title == searchStr ||);

                return View(posts);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Person person)
        {
            if(person != null)
            {
                var user = await blogDbContext.Persons.FirstAsync(u => u.Email == person.Email 
                                                        && u.Password == person.Password);
                return RedirectToAction("PersonalArea", "People", new { id = user.Id});
            }
            return View(TempData["CheckUser"] = "No users!");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}