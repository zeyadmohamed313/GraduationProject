using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class ApplicationUserDTO
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public String Email {  get; set; }
		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }
		[Required]
		public String Major {  get; set; }
	}
}
