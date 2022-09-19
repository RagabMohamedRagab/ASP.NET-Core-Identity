using AutoMapper;
using Identity.DAL.Context;
using Identity.DAL.ViewModel;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity {
    public class Startup {
        readonly IConfiguration Configurtion;

        public Startup(IConfiguration configurtion)
        {
            Configurtion = configurtion;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Configurtion.GetConnectionString("Identity")));
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IFileService, FileService>();
            services.AddMvc(option =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                   .RequireAuthenticatedUser()
                                   .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase= true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"Identity",
                    pattern: "{Controller=Account}/{Action=Index}/{id?}"
                    );
            });
        }
    }
}
