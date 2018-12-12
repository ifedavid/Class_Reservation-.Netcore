using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Classes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Classes.Pages
{
    [Authorize]

    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;
        public ClassModel ChosenClass { get; set; }
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> signInManager;
        public Reserved_class reserved_Class = new Reserved_class();

        public ContactModel(CRUD_Classes.Models.CRUD_ClassesContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ClassModel ClassModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? Id)
        {


            if (Id == null)
            {
                return NotFound();
            }

            ClassModel = await _context.ClassModel.SingleOrDefaultAsync(m => m.Id == Id);
            ChosenClass = _context.ClassModel.SingleOrDefault(m => m.Id == Id);

            reserved_Class.Class = ChosenClass;


            var currentUser = await _userManager.GetUserAsync(User);
            var currentClass = _context.Reserved_class.Where(m => m.User == currentUser).Select(c => c.Class == ChosenClass);
            
            reserved_Class.User = currentUser;

            ChosenClass.Capacity = ChosenClass.Capacity - 1;
            if (ChosenClass.Capacity <= 0)
            {
                ChosenClass.Capacity = 0;
                Message = "Class Is Already at full capacity";
                return Page();
            }
            else if (ChosenClass.EndTime >= DateTime.Today)
            {
                Message = "Class is expired";
                return Page();
            }
            else if(currentClass.Count()>=1)
            {

                Message = "You Already reserved this class";
                return Page();
            }
            else
            {
                _context.Reserved_class.Add(reserved_Class);
                await _context.SaveChangesAsync();

                Message = "Class reserved successfully";
                return Page();

            }


           


        }

   

    }
}

