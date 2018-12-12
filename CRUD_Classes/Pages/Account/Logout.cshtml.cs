using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Classes.Pages.Account
{
    public class LogoutModel : PageModel
    {

        public SignInManager<ApplicationUser> _signInManager;
        public UserManager<ApplicationUser> _userManager;
        public LogoutModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGet()
        {

            await _signInManager.SignOutAsync();

          
  
                return RedirectToPage("/Home/Index");
           
        }

       
    }
}