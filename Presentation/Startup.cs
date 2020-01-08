using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PollutionReports.Models;
using PollutionReports.Implementation;
using PollutionReports.Interfaces;

namespace Presentation
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
            
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("PollutionReportsDBConnection")));
            services.AddIdentity<User, ApplicationRole>(options =>
                    {
                        options.Password.RequiredLength = 10;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredUniqueChars = 3;
                    })
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();                       

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddScoped<ICompanyRepository, SQLCompany>();
            services.AddScoped<IReportRepository, SQLReport>();
            services.AddScoped<ISurveyRepository, SQLSurvey>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              AppDbContext context, UserManager<User> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
                
            });
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
           FillRoles.Initialize(context, userManager, roleManager).Wait();
        }
    }
}
