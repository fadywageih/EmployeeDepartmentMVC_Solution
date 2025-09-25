using EmployeeDepartment.BLL.Common.Services;
using EmployeeDepartment.BLL.Common.Services.EmailSettings;
using EmployeeDepartment.BLL.Services.Department;
using EmployeeDepartment.BLL.Services.Employee;
using EmployeeDepartment.DAL.Models.Identity;
using EmployeeDepartment.DAL.Presistance.Data;
using EmployeeDepartment.DAL.Presistance.UnitOfWork;
using EmployeeDepartmentMVC.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartmentMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {

                options.UseLazyLoadingProxies().
                UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService,AttachmentService>();
            builder.Services.AddScoped<IEmailSettings, EmailSettings>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>((option)=>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = true;
                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.MaxFailedAccessAttempts = 7; //???? ??????? ??? ?? ??? ????? ??? 7 ???? 
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/SignIn";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            #endregion

            // Add services to the container.

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
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
