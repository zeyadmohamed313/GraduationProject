using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class FavouriteList
	{
		[Key]
		public int Id { get; set; }

		// Foreign key to reference the User
		[Required]
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }
		[Required]
		public string Name { get; set; }

		// Navigation property for the related User
		public ApplicationUser ApplicationUser { get; set; }
		public List<Book> Books { get; set; }

	}
}
