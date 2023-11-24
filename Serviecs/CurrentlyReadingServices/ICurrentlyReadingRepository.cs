using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public interface ICurrentlyReadingRepository
	{
		CurrentlyReading GetById(int id);
		public CurrentlyReading GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyCurrentlyReadingList(string UserID);
	    List<BookDTO> SearchForBooks(string id, string Name);
		public void AddCurrentlyReadingListToUser(CurrentlyReading currentlyReading);
		public void AddBook(string UserID, int BookID);
		public void DeleteBook(string UserID, int BookID);
	}
}
