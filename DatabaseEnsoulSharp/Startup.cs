using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Services;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseEnsoulSharp
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
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            services.AddSingleton<IMyCache, MyMemoryCache>();
            services.AddSingleton<IChampionScriptService, ChampionScriptService>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IRatingService, RatingService>();
            services.AddSingleton<IFirebaseService, FirebaseService>();
            services.AddSingleton<IResponseServerService, ResponseServerService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IChampionService, ChampionService>();
            services.AddSingleton<IScriptInfoService, ScriptInfoService>();


            
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
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}