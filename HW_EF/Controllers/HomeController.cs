using Humanizer;
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
            return View(blogDbContext.Posts);
        }

        //[HttpGet]
        //public IActionResult Search()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Search(string searchStr) //TODO: сделать так, чтобы поиск выполнялся по набору букв, а не по идентичному названию
        {
            if(!string.IsNullOrWhiteSpace(searchStr))
            {
                var request = blogDbContext.Posts.Where(t => t.Title == searchStr);

                return View(request);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Person person)
        {
            if(person != null)
            {
                var user = await blogDbContext.Persons.FirstAsync(u => u.Email == person.Email 
                                                        && u.Password == person.Password);
                //var posts = blogDbContext.Posts.Where(p => p.PersonId == user.Id);

                //var personalArea = new PersonalArea()
                //{
                //    User = user,
                //    Posts = posts
                //};
                return RedirectToAction("PersonalArea", "People", new { id = user.Id});
            }

            return View(TempData["CheckUser"] = "No users!");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(Post post)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        post.Date = DateTime.Now;
        //        await blogDbContext.AddAsync(post);
        //        await blogDbContext.SaveChangesAsync();
        //        TempData["Status"] = "New post added";
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View(post);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}