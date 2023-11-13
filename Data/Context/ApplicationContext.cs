using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Context
{
	public class ApplicationContext:IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext()
		{
		}
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.
				UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=GraduationProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<FavouriteList> FavouriteLists { get; set; }
		public DbSet<Plan> Plans { get; set; }
		public DbSet<Notes> Notes { get; set; }
		public DbSet<MyPlan> MyPlans { get; set; }
		public DbSet<CurrentlyReading> CurrentlyReadings { get; set; }
		public DbSet<ToRead> ToReads { get; set; }

		public DbSet<Read> Reads { get; set; }


	}
}
