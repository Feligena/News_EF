using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW_EF.Models;
using HW_EF.ViewModel;
using HW_EF.Middlewares.Services;
using HW_EF.Encryptors;

namespace HW_EF.Controllers
{
    public class PeopleController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly IUserManager userManager;

        public PeopleController(BlogDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (userManager.Login(model.Email, model.Password))
                {
                    RedirectToAction("PersonalArea", "People", new { email = model.Email });
                }

            }

            ModelState.AddModelError("all", "Incorrect username or password!");

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> PersonalArea(string email)
        {
            var user = await _context.Persons
                             .Include(p => p.UserPosts)
                             .FirstOrDefaultAsync(m => m.Email == email);

            if(user == null)
            {
                return NotFound();
            }

            var personalArea = new PersonalArea
            {
                User = user,
                Posts = user.UserPosts
            };

            return View(personalArea);
        }

		// GET: People/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.Persons.AddAsync(new Person
                {
                    Email = model.Email,
                    PasswordHash = Sha256Encryptor.Encrypt(model.Password),
                    IsAdmin = false
                });
                await _context.SaveChangesAsync();



                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

            //     [HttpPost]
            //     [ValidateAntiForgeryToken]
            //     public async Task<IActionResult> Create([Bind("Id,Name,Surname,Nickname,Password,Email,Phone")] Person person, string password2)
            //     {
            //         if(person.Password == password2)
            //         {
            //             var checkEmail = _context.Persons.First(p => p.Email == person.Email);
            //             if(checkEmail == null)
            //             {
            //                 if (ModelState.IsValid)
            //                 {
            //                     _context.Add(person);
            //                     await _context.SaveChangesAsync();
            //                     return RedirectToAction("PersonalArea", "People", person);
            //                 }
            //             }
            //             else
            //             {
            //                 TempData["chackEmail"] = "This email is already taken";
            //                 return View(person);
            //             }

            //}

            //         return View(person);
            //     }

            // GET: People/Edit/5

            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Nickname,Password,Email,Phone")] Person person, string password2)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            //if(person.Password == password2)
            {
				if (ModelState.IsValid)
				{
					try
					{
						_context.Update(person);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!PersonExists(person.Id))
						{
							return NotFound();
						}
						else
						{
							throw;
						}
					}
					return RedirectToAction("ListUsers", "People");
				}
			}

            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'BlogDbContext.Persons'  is null.");
            }
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ListUsers", "People");
        }

        private bool PersonExists(int id)
        {
          return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
