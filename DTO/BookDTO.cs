using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class BookDTO
	{
		public int ID { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Author")]
		public string? Author { get; set; }

		[Required]
		[StringLength(300)]
		[Display(Name = "Link")]
		public string? GoodReadsUrl { get; set; }

		[StringLength(500)]
		[Display(Name = "Description")]
		public string? Description { get; set; }
		public int CategoryId { get; set; }
	}
}
