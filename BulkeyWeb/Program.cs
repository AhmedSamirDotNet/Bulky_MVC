using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bulky.Utility;

namespace BulkeyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add services for razor pages
            builder.Services.AddRazorPages();

            //When an instance of IEmailSender is requested, an instance of EmailSender will be provided.
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            //builder.Services.AddScoped<ICategoryRepository,CategoryRepository>(); //When an iلاعهnstance of ICategoryRepository is requested, an instance of CategoryRepository will be provided.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//When an instance of IUnitOfWork is requested, an instance of UnitOfWork will be provided.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //Add after AddIdentity method because it is used to configure the application cookie.
            //Configure routes for login, logout and access denied because we are using areas in our project. So we need to specify the paths for these actions.  
            builder.Services.ConfigureApplicationCookie(builder =>
            {
                builder.LoginPath = $"/Identity/Account/Login";
                builder.LogoutPath = $"/Identity/Account/Logout";
                builder.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();//Map Razor pages
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();


            DoubleTheNumber(5);
        }
        public static Func<int, int> DoubleTheNumber = (x => x * 2);

    }

}
