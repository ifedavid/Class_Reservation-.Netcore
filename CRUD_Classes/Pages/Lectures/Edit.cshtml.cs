using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Classes.Models;

namespace CRUD_Classes
{
    public class EditModel : PageModel
    {
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;

        public EditModel(CRUD_Classes.Models.CRUD_ClassesContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ClassModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassModelExists(ClassModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClassModelExists(int id)
        {
            return _context.ClassModel.Any(e => e.Id == id);
        }
    }
}
