using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Models;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Utility;

namespace WarehouseManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        // Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Set up identity system
            // Needed for authentication - Registration and Role assignemnt
            services.AddIdentity<IdentityUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultUI(UIFramework.Bootstrap4)
                   .AddDefaultTokenProviders();

            // Use Core 2.2
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Registers DB Context
            // Connects to service so it can be used as a connection between the database and code
            services.AddDbContext<WholesaleContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WholesaleContext")));

            services.AddDbContext<WarehouseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WarehouseContext")));

            services.AddDbContext<StoreContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StoreContext")));

            // Register dependency injection
            // Can use database functionality from controllers (Utility folder)
            services.AddTransient<IOrderServiceStore, OrderServiceStore>();
            services.AddTransient<IOrderServiceWarehouse, OrderServiceWarehouse>();

        }

        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. 
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(services).Wait();
        }

        //method which will be responsible for the creation of specific user roles
        private async Task CreateUserRoles(IServiceProvider serviceProvider)  
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;

            // Adding Admin Role
            var wm = await RoleManager.RoleExistsAsync("WarehouseManager");
            var sm = await RoleManager.RoleExistsAsync("StoreManager");

            // If roles don't exist
            if (!wm)
            {
                // Create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("WarehouseManager"));
            }
            else if (!sm) { 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("StoreManager"));
            }
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            IdentityUser warehouseUser = await UserManager.FindByEmailAsync("warehouseManager@gmail.com");
            IdentityUser storeUser = await UserManager.FindByEmailAsync("storeManager@gmail.com");

            // Assign users to roles
            var User = new IdentityUser();
            await UserManager.AddToRoleAsync(warehouseUser, "WarehouseManager");
            await UserManager.AddToRoleAsync(storeUser, "StoreManager");
        }
    }
}
