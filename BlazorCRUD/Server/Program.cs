using BlazorCrud.DataAccess;
using BlazorCrud.DataAccess.Repositories;
using BlazorCrud.DataAccess.Repositories.Interfaces;
using BlazorCRUD.DataAccess.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var corsPolicyName = "CorsPolicy";
            builder.Services.AddCors(options => options.AddPolicy(corsPolicyName, policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
            // Uncomment the Repository to be used - Can only ever have one implementation registered for an interface.
            //builder.Services.AddSingleton<IMoviesRepository, MoviesInMemoryRepository>();
            //builder.Services.AddScoped<IMoviesRepository, MoviesEntityFrameworkRepository>();
            builder.Services.AddScoped<IMoviesRepository, MoviesDapperRepository>();
            builder.Services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(corsPolicyName);
            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}