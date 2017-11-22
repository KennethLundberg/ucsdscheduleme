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
//                options.Filters.Add(new RequireHttpsAttribute());
            });

//            services.AddDbContext<ScheduleContext>(options =>
 //               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
services.AddDbContext<ScheduleContext>(options =>
                options.UseSqlServer(@"Data Source=usm.c97rq5qtindm.us-west-2.rds.amazonaws.com;Initial Catalog=usm;User Id=uadmin;Password=testtest;"));
            


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ScheduleContext>()
                .AddDefaultTokenProviders();

//            services.AddAuthentication().AddGoogle(googleOptions =>
  //          { 
   //             googleOptions.ClientId = "674220527555-k9cls5f078h61jmg770urecjhun7v4ml.apps.googleusercontent.com";
     //           googleOptions.ClientSecret = "HKLgURgNLWoJS5KXlfjqJ4Kk";
       //     });

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

           // app.UseRewriter(options);

            app.UseStaticFiles();

//            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
