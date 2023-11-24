using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
	public class ApplicationUser :IdentityUser
	{

		[Required]
		[StringLength(50)]
		[Display(Name = "Major")]
		public string Major { get; set; }

		public List<MyPlan>? MyPlans { get; set; } = new List<MyPlan>();


		public CurrentlyReading? CurrentlyReading { get; set; }

		public ToRead? ToReadList { get; set; }

		public Read? ReadList { get; set; }

		public List<Notes>? Notes { get; set; } = new List<Notes>();

		public FavouriteList? FavouriteList { get; set; }

	}
}
