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
		// we add it in user repo to generate it automatically
		public CurrentlyReading CurrentlyReading { get; set; }
		public ToRead ToReadList { get; set; }
		public Read ReadList { get; set; }
		public List<Notes>? Notes { get; set; }
		public FavouriteList FavouriteList { get; set; }

	}
}
