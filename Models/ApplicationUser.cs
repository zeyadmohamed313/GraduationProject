using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class ApplicationUser :IdentityUser
	{

		[Required]
		[StringLength(50)]
		[Display(Name = "Major")]
		public string Major { get; set; }
        
		public List<MyPlan>? MyPlans { get; set; }
		public CurrentlyReading? CurrentlyReading { get; set; }
		public List<Notes>? Notes { get; set; }
		public FavouriteList? FavouriteList { get; set; }

	}
}
