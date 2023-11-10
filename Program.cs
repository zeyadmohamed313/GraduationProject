
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data.Context;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CategoryServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using GraduationProject.Serviecs.FavouriteListServices;
using GraduationProject.Serviecs.MyPlanServices;
using GraduationProject.Serviecs.PlanServices;
using GraduationProject.Serviecs.NotesServices;

namespace GraduationProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
			builder.Services.AddScoped<IBookRepository, BookRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ICurrentlyReadingRepository, CurrentlyReadingRepository>();
			builder.Services.AddScoped<IFavouriteListRepository, FavouriteListRepository>();
			builder.Services.AddScoped<IMyPlanRepository, MyPlanRepository>();
			builder.Services.AddScoped<INotesRepository, NotesRepository>();
			builder.Services.AddScoped<IPlanRepository, PlanRepository>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}