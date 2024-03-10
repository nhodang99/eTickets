using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class AccountController(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               AppDbContext context) : Controller
{
   private readonly UserManager<ApplicationUser> _userManager = userManager;
   private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
   private readonly AppDbContext _context = context;

   public IActionResult Login() => View(new LoginVM());

   [HttpPost]
   public async Task<IActionResult> Login(LoginVM loginVM)
   {
      if (!ModelState.IsValid)
      {
         return View(loginVM);
      }
      var user = await _userManager.FindByEmailAsync(loginVM.Email);
      if (user is not null)
      {
         bool passwordValid = await _userManager.CheckPasswordAsync(user, loginVM.Password);
         if (passwordValid)
         {
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(Index), "Movies");
            }
         }
      }
      TempData["Error"] = "Wrong credential. Please try again.";
      return View(loginVM);
   }


   public IActionResult Register() => View(new RegisterVM());

   [HttpPost]
   public async Task<IActionResult> Register(RegisterVM registerVM)
   {
      if (!ModelState.IsValid) return View(registerVM);

      var user = await _userManager.FindByEmailAsync(registerVM.Email);
      if (user is not null)
      {
         TempData["Error"] = "This email is already in use";
         return View(registerVM);
      }

      var newUser = new ApplicationUser
      {
         FullName = registerVM.FullName,
         Email = registerVM.Email,
         UserName = registerVM.Email
      };
      var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

      if (newUserResponse.Succeeded)
      {
         await _userManager.AddToRoleAsync(newUser, UserRoles.User);
      }

      return View("RegisterCompleted");
   }

   [HttpPost]
   public async Task<IActionResult> Logout()
   {
      await _signInManager.SignOutAsync();
      return RedirectToAction(nameof(Index), "Movies");
   }

   public async Task<IActionResult> Users()
   {
      var users = await _context.Users.ToListAsync();
      return View(users);
   }

   public IActionResult AccessDenied(string returnUrl)
   {
      return View();
   }
}