using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Dtos.Account;
using Movies.Models.Models.Identity;

namespace Movies.App.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHtmlSanitizer _htmlSanitizer;
    
    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        
        _htmlSanitizer = new HtmlSanitizer();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Login() => View(new LoginDto());
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = await this._userManager.FindByEmailAsync(this._htmlSanitizer.Sanitize(model.Email));

        if (user == null) return RedirectToAction(nameof(Register));
        
        var isSucceeded = await this.SignInUserAsync(user, model);

        return isSucceeded 
            ? this.RedirectToAction("Index", "Home") 
            : RedirectToAction(nameof(Login));
    }
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Register() => View(new RegisterDto());
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = new ApplicationUser()
        {
            UserName = this._htmlSanitizer.Sanitize(model.Email),
            Email = this._htmlSanitizer.Sanitize(model.Email),
        };

        var result = await this._userManager.CreateAsync(user, this._htmlSanitizer.Sanitize(model.Password));

        if (result.Succeeded)
        {
            await this._signInManager.SignInAsync(user, isPersistent: false);
            await this._userManager.AddToRoleAsync(user, "User");

            return this.RedirectToAction(nameof(this.Login));
        }

        foreach (var item in result.Errors)
        {
            this.ModelState.AddModelError(string.Empty, item.Description);
        }

        return this.View(model);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await this._signInManager.SignOutAsync();
        return this.RedirectToAction("Index", "Home");
    }
    
    private async Task<bool> SignInUserAsync(ApplicationUser user, LoginDto model)
    {
        var result = await this._signInManager.PasswordSignInAsync(user, model.Password, false, false);

        return result.Succeeded;
    }
}