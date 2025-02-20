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