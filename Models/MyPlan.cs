using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
	public class MyPlan
	{
		[Key]
		public int Id { get; set; }

		// Foreign key to reference the User
		[Required]
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }

		// Navigation property for the related User
		public ApplicationUser ApplicationUser { get; set; }

		// A collection of plans associated with the user
		public List<Plan> Plans { get; set; } = new List<Plan>();
	}
}
