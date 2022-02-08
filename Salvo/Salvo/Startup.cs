using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo
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
            // Inyeccion de dependencia para salvo context
            services.AddDbContext<SalvoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SalvoDataBase")
                ,o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            // Inyectar Repositorio de Game
            services.AddScoped<IGameRepository, GameRepository>();
            // Inyectar Repositorio de GamePlayer
            services.AddScoped<IGamePlayerRepository, GamePlayerRepository>();
            // Inyectar Repositoria de Player
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            // Inyectar Repositorio de Score
            services.AddScoped<IScoreRepository, ScoreRepository> ();



            // Autenticacion
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.LoginPath = new PathString("/index.html");
                });
            // Autorizacion
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PlayerOnly", policy => policy.RequireClaim("Player"));
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
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            // Usar Autenticacion
            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=games}/{ action = Get}");
            }); 
        }
    }
}
