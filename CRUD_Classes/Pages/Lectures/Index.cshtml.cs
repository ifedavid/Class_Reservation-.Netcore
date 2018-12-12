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
    public class IndexModel : PageModel
    {
        private readonly CRUD_Classes.Models.CRUD_ClassesContext _context;

        public IndexModel(CRUD_Classes.Models.CRUD_ClassesContext context)
        {
            _context = context;
        }

        public IList<ClassModel> ClassModel { get;set; }

        public async Task OnGetAsync()
        {
            ClassModel = await _context.ClassModel.ToListAsync();
        }
    }
}
