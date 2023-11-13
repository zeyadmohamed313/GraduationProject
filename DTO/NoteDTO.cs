using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class NoteDTO
	{
		public int Id { get; set; }
		[Required]
		public int BookId { get; set; }
		[Required]
		public String UserId { get; set; }
		[Required]
		public int PageNumber { get; set; }
		[MaxLength]
		public string NoteText { get; set; }
	}
}
