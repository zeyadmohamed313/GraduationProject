using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public interface IToReadRepository
	{
		ToRead GetById(int id);
		public ToRead GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyToReadsList(string UserID);
		public List<BookDTO> SearchForBooks(string UserID,string Name);

		public void AddToReadToUser(ToRead toread);

		public void AddBook(string UserID, int BookID);
		public void DeleteBook(string UserID, int BookID);
	}
}
