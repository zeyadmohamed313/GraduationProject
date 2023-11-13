using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public interface IToReadRepository
	{
		ToRead GetById(int id);
		public ToRead GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyToReadsList(int id);
		public void AddBook(int ToReadListID, int BookID);
		public void DeleteBook(int ToReadListID, int BookID);
	}
}
