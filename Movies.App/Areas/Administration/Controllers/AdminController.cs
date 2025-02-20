using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models.Models.Identity;

namespace Movies.App.Areas.Administration.Controllers;

[Area("Administration")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }

    #region Manage Users
    
    [HttpGet]
    public async Task<IActionResult> ManageUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }
    
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(ApplicationUser model, string password)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return RedirectToAction("ManageUsers");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(ApplicationUser model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();

        user.Email = model.Email;
        user.UserName = model.Email;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return RedirectToAction(nameof(ManageUsers));

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AssignRole(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        ViewBag.Roles = _roleManager.Roles.ToList();
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(string id, string roleName)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        if (await _roleManager.RoleExistsAsync(roleName))
            await _userManager.AddToRoleAsync(user, roleName);

        return RedirectToAction(nameof(ManageUsers));
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userManager.DeleteAsync(user);
        return RedirectToAction(nameof(ManageUsers));
    }
    
    #endregion

    #region Manage Roles

    [HttpGet]
    public async Task<IActionResult> ManageRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    
    [HttpGet]
    public IActionResult CreateRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(ApplicationRole model)
    {
        if (ModelState.IsValid)
        {
            var role = new ApplicationRole { Name = model.Name };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return RedirectToAction("ManageRoles");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(ApplicationRole model)
    {
        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role == null) return NotFound();

        role.Name = model.Name;

        var result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
            return RedirectToAction("ManageRoles");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        await _roleManager.DeleteAsync(role);
        return RedirectToAction(nameof(ManageRoles));
    }
    
    #endregion
}