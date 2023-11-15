using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class Plan
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
		[Required]
		[StringLength(500)]
		public string Description { get; set; }

		public string ImgUrl { get; set; } = string.Empty;
		// A collection of books associated with the plan
		public List<Book> Books { get; set; } = new List<Book>();
	}
}
