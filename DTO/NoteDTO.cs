using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class NoteDTO
	{
		[Required]
		public int PageNumber { get; set; }
		[MaxLength]
		public string NoteText { get; set; }
	}
}
