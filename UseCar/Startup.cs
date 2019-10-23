using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UseCar.Filter;
using UseCar.Helper;
using UseCar.Models;
using UseCar.Repositories;

namespace Car_Somchai
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddDbContext<UseCarDBContext>(options =>
        options.UseMySql(Configuration.GetConnectionString("UseCarDBContext")));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddHttpContextAccessor();

            services.AddMvc(options => {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new UseCarActionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<DropdownList>();
            services.AddTransient<SharedData>();
            services.AddTransient<FileManagement>();
            services.AddTransient<ActionCar>();
            services.AddTransient<DepartmentManagementRepository>();
            services.AddTransient<UserManagementRepository>();
            services.AddTransient<PermissionManagementRepository>();
            services.AddTransient<ManageBranchRepository>();
            services.AddTransient<CarSettingRepository>();
            services.AddTransient<RepairShopRepository>();
            services.AddTransient<VendorRepository>();
            services.AddTransient<ReceiveCarRepository>();
            services.AddTransient<MaintenanceCarRepository>();
            services.AddTransient<CheckupSettingRepository>();
            services.AddTransient<CheckupCarRepository>();
            services.AddTransient<CarRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
