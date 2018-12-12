using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CRUD_Classes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace CRUD_Classes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
               // options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc();

            services.AddDbContext<CRUD_ClassesContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CRUD_ClassesContext")));

            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<CRUD_ClassesContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = true;

            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

           /// app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
           
            app.UseCookiePolicy();
            app.UseMvc();




            CreateUserRoles(service).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider service)
        {
            var RoleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            IdentityResult roleResult;
            string[] roleNames = { "User", "Admin" };
            foreach (var Rolename in roleNames)
            {
                bool roleCheck = await RoleManager.RoleExistsAsync(Rolename);
                if (!roleCheck)
                {

                    roleResult = await RoleManager.CreateAsync(new IdentityRole(Rolename));
                }
            }
            ApplicationUser user1 = await UserManager.FindByEmailAsync("ife@ife.com");
            if (user1 == null)
            {
                user1 = new ApplicationUser() { UserName = "ife@ife.com", Email = "ife@ife.com" };

                await UserManager.CreateAsync(user1, "ifeoluwa");
            }
            await UserManager.AddToRoleAsync(user1, "Admin");


            ApplicationUser user2 = await UserManager.FindByEmailAsync("ife@i.com");
            if (user2 == null)
            {
                user2 = new ApplicationUser() { UserName = "ifeoluwa@r.com", Email = "ifeoluwa@r.com" };

                await UserManager.CreateAsync(user2, "ifeoluwa");
            }
            await UserManager.AddToRoleAsync(user2, "User");
        
    }
    }
}
