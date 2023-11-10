using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
	public class Book
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Author")]
		public string Author { get; set; }

		[Required]
		[StringLength(300)]
		[Display(Name = "Author")]
		public string GoodReadsUrl { get; set; }

		[StringLength(500)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Image URL")]
		public string ImgUrl { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Release Date")]
		public DateTime Date { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public List<Notes> Notes { get; set; }
	}
}
