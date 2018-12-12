using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Classes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Classes.Pages.Account
{
    [Authorize]
    public class Manage_ClassesModel : PageModel
    {

        public UserManager<ApplicationUser> _userManager;
        private readonly CRUD_ClassesContext _context;
        public Reserved_class reserved;
        public ClassModel classModel;
        public IList<ClassModel> classModels;
        public Manage_ClassesModel(UserManager<ApplicationUser> userManager, CRUD_ClassesContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGetAsync()

        {
            var Current_user = await _userManager.GetUserAsync(User);

            if (Current_user == null)
            {
                RedirectToPage("/Account/Login");
            }
            else
            {

                
                 classModels = _context.Reserved_class.Where(m => m.User == Current_user).Select(c => c.Class).Distinct().ToList();
               
                
            }
           
        
            
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var Current_user = await _userManager.GetUserAsync(User);

            
            if (id == null)
            {
                return NotFound();
            }


            classModel = _context.ClassModel.SingleOrDefault(m => m.Id == id);
            
            var somethingelse = _context.Reserved_class.Where(m => m.User == Current_user).FirstOrDefault(c => c.Class == classModel);
            if (somethingelse != null)
            {

                _context.Reserved_class.Remove(somethingelse);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToPage("/Account/Manage_Classes");
        }
    }
}