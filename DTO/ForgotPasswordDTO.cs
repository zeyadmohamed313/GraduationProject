using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class ForgotPasswordDTO
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string NewPassword { get; set; }
	}
}
