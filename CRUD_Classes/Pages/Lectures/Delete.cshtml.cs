using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUD_Classes.Models;

namespace CRUD_Classes
{
    public class DeleteModel : PageModel
    {
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;

        public DeleteModel(CRUD_Classes.Models.CRUD_ClassesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassModel ClassModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassModel = await _context.ClassModel.SingleOrDefaultAsync(m => m.Id == id);

            if (ClassModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassModel = await _context.ClassModel.FindAsync(id);

            if (ClassModel != null)
            {
                _context.ClassModel.Remove(ClassModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
