using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ucsdscheduleme.Data;
using Microsoft.EntityFrameworkCore;
using ucsdscheduleme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;

namespace ucsdscheduleme
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
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
                
            // Remote database
            services.AddDbContext<ScheduleContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                options.UseSqlServer(@"Data Source=usm.c97rq5qtindm.us-west-2.rds.amazonaws.com;Initial Catalog=usm;User Id=uadmin;Password=testtest;"));
       
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ScheduleContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "226496862693-jt3ln0g49bsusoo9h0f1b8hi8u4aee8k.apps.googleusercontent.com";
                googleOptions.ClientSecret = "ZFKYjlsmqqtsXqiFnHyRglks";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var options = new RewriteOptions()
                .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
