using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
	public class Notes
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[ForeignKey("Book")]
		public int BookId { get; set; }
		[Required]
		[ForeignKey("ApplicationUser")]
		public String UserId { get; set; }
		[Required]
		public int PageNumber { get; set; }
		[MaxLength]
		public string NoteText { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public Book Book { get; set; }
	}
}
