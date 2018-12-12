using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUD_Classes.Models;

namespace CRUD_Classes
{
    public class CreateModel : PageModel
    {
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;

        public CreateModel(CRUD_Classes.Models.CRUD_ClassesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ClassModel ClassModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ClassModel.Add(ClassModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}