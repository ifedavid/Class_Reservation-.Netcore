using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Classes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Classes.Pages
{
    [Authorize]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> signInManager;
        public AboutModel(CRUD_Classes.Models.CRUD_ClassesContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ClassModel> ClassModels { get; set; }
        [BindProperty]
        public ClassModel ChosenClass { get; set; }
        public Reserved_class reserved_Class = new Reserved_class();
        public async Task OnGet()
        {

            ClassModels = _context.ClassModel.ToList();
        }
        [BindProperty]
        public int Classid { get; set; }
        /*  public async Task<IActionResult> OnPostAsync(int? Id)
          {


              if (Id == null)
              {
                  return NotFound();
              }

              ChosenClass = _context.ClassModel.SingleOrDefault(m => m.Id == Id);

              reserved_Class.Class = ChosenClass;


              var user = await _userManager.GetUserAsync(User);

              reserved_Class.User = user;

              ChosenClass.Capacity = ChosenClass.Capacity - 1;
            ///  if (ChosenClass.Capacity <= 0)
             /// {
              //    ChosenClass.Capacity = 0;
              ///    Message = "Class Is Already at full capacity";
             ////     return Page();
             // }
             /// else if (ChosenClass.EndTime >= DateTime.Today)
            ///  {
                 /// Message = "Class is expired";
                 // return Page();
            ///  }
            ///  else 
             // {
                  _context.Reserved_class.Add(reserved_Class);
                  await _context.SaveChangesAsync();

                  Message = "Class reserved successfully";
                  return Page();

             // }
          }


          }*/
    }
        
    }


