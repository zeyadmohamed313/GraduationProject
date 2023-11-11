using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class CategoryDTO
	{
		[Required]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }

	}
}
