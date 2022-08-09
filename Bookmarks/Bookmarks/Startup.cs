using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmarks.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bookmarks
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
            services.AddRazorPages();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddOptions<MenuOptions>().Configure<IConfiguration>(
                (options,conf) => {
                    options.Items = conf.GetSection("Menu").GetChildren().ToList().Select(x => new MenuItem {
                        Name = x.GetValue<string>("Name"),
                        Label = x.GetValue<string>("Label"),
                        Link = x.GetValue<string>("Link")
                    }).ToList();
                }
            );
            services.AddControllersWithViews(options => { 
                options.Filters.Add<ViewBagActionFilter>();
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
