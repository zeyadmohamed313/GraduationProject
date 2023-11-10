﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class CurrentlyReading
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		// Collection of books associated with the currently reading list
		public List<Book> Books { get; set; }

		// Navigation property for the related User
		public ApplicationUser ApplicationUser { get; set; }
	}
}
