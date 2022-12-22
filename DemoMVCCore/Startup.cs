using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DemoMVCCore.Model;
using DemoMVCCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using DemoMVCCore.Model;

namespace DemoMVCCore
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
                ).AddXmlDataContractSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // dependancy injection - register
            // AddSingleton - one shared instance across all sessions
            ///services.AddSingleton<IEmployeeRepo, MockEmployleeRepo>();
            services.AddScoped<IEmployeeRepo, DBEmployeeRepo>();
            //services.AddTransient<IEmployeeRepo, MockEmployleeRepo>();
            //AddTransient - each time per session has its own instance
            //AddScoped - once by request within request ... then same instance shared (per session)
            //services.AddScoped<IEmployeeRepo, MockEmployleeRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                //app.UseStatusCodePages(); // ugly - rarely used
                //app.UseStatusCodePagesWithRedirects("/Error/{0}"); // no real error
                //app.UseStatusCodePagesWithReExecute("/Error/{0}"); // preserve original url

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseMvc(); // attribute routing
            //app.UseMvc(routes => {
            //    routes.MapRoute("default", "{controller=Home}/{action=getall}/{id?}");
                ///routes.MapRoute("duet", "{controller}/{action}");
            //});
            //app.UseMvcWithDefaultRoute();

        }
    }
}
