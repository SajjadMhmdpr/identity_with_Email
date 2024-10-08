using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using identity_DataLayer.Context;
using identity_with_Email.ViewModels;
using Microsoft.AspNetCore.Identity;
using identity_with_Email.Models;

namespace identity_with_Email.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContext _context;

        public UserController( UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.Select(u=>new UserVM()
            {
                Id = u.Id,
                Email = u.Email,
                Phone = u.PhoneNumber,
                UserName = u.UserName
            });
            return View(await users.ToListAsync());
        }
        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var r= await _roleManager.Roles.ToListAsync();
            ViewBag.roles = r.Select(rr => rr.Name);
            ViewBag.userRole = await _userManager.GetRolesAsync(user);
            return View(new UserVM()
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                Phone = user.PhoneNumber
            });
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserVM model , List<string> selectedRoles)
        {

            if (!ModelState.IsValid || selectedRoles == null)
            {
                ModelState.AddModelError("","اطلاعات را به درستی وارد کنید");
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            await _userManager.UpdateAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, selectedRoles);

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");

        }
    }
}
