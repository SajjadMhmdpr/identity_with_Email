using identity_DataLayer.Context;
using identity_with_Email.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace identity_with_Email.Controllers
{
    public class ManageRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.Select(r => new RoleVM()
            {
                Id = r.Id,
                Name = r.Name
            });
            return View(await roles.ToListAsync());
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
        public async Task<IActionResult> Create(RoleVM role)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","فیلد هارا به درستی پر کنید");
                return View(role);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = role.Name
            });
            
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(role);
            }

            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id) ;
            if (role == null)
            {
                return NotFound();
            }
            return View(new RoleVM()
            {
                Name = role.Name,
                Id = role.Id
            });
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleVM role)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","فیلد ها را به درستی پر کنید");
                return View(role);
            }

            var r = await _roleManager.FindByIdAsync(role.Id);
            r.Name = role.Name;
            var res =await _roleManager.UpdateAsync(r);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(role);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

    }
}
