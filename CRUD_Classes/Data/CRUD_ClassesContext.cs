using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Classes.Models
{
    public class CRUD_ClassesContext : IdentityDbContext<ApplicationUser>
    {
        public CRUD_ClassesContext(DbContextOptions<CRUD_ClassesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CRUD_Classes.Models.ClassModel> ClassModel { get; set; }

       public DbSet<Reserved_class> Reserved_class { get; set; }
    }
}
