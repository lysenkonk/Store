using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Identity;
using Store.Services;

namespace Store
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:StoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(Configuration["Data:StoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddTransient<ProductsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseSession();
                app.UseAuthentication();
                app.UseMvc(routes =>{
                    routes.MapRoute(
                        name: "delivery",
                        template: "Delivery",
                        defaults: new { controller = "StaticViews", action = "Delivery" }
                    );
                    routes.MapRoute(
                        name: "feedback",
                        template: "Feedback",
                        defaults: new { controller = "StaticViews", action = "Feedback" }
                    );                 
                    routes.MapRoute(
                        name: "about",
                        template: "About",
                        defaults: new { controller = "StaticViews", action = "About" }
                    );
                    routes.MapRoute(
                        name: "admin",
                        template: "Admin/{action=Index}/{productId?}",
                        defaults: new { controller = "Admin" }
                    );
                    routes.MapRoute(
                        name: "account",
                        template: "Account/{action=Login}",
                        defaults: new { controller = "Account" }
                    );
                    routes.MapRoute(
                        name: "product",
                        template: "Product/{id}",
                        defaults: new { controller = "Product", action = "Product" }
                        );
                    routes.MapRoute(
                        name: "products",
                        template: "Products/{page}",
                       defaults: new { controller = "Product", action = "List" }

                    );
                    routes.MapRoute(
                        name: "category",
                        template: "{category}/{page}",
                        defaults: new { controller = "Product", action = "List" }
                        );
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Product}/{action=List}/{page?}");
                });
            //}
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app).Wait();
        }
    }
}
