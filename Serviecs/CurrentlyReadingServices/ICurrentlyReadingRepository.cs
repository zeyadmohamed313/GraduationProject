﻿using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public interface ICurrentlyReadingRepository
	{
		CurrentlyReading GetById(int id);
		public CurrentlyReading GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyCurrentlyReadingList(int id);
		public void AddBook(int CurrentlyReadingsListID, int BookID);
		public void DeleteBook(int CurrentlyReadingsListID, int BookID);
	}
}
