using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class Category
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; }
		[Display(Name="Descreption")]
		public string Description { get; set; }

		// Navigation property for the list of associated books
		public List<Book>? Books { get; set; } = new List<Book>();
	}
}
