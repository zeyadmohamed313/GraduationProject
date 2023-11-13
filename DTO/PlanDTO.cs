﻿using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
	public class PlanDTO
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
		[Required]
		[StringLength(500)]
		public string Description { get; set; }
	}
}
