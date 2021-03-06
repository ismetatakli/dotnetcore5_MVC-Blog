using AutoMapper;
using Blog.Mvc.AutoMapper.Profiles;
using Blog.Services.AutoMapper.Profiles;
using Blog.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blog.Mvc
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt=> {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile));
            services.AddRazorPages();
            services.LoadMyServices();
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder {
                    Name = "MyBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // must be always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(4);
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
            });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern:"Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
