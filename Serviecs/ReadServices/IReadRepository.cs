using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.ReadServices
{
	public interface IReadRepository
	{
		Read GetById(int id);
		public Read GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyReadsList(int id);
		public void AddBook(int ReadListID, int BookID);
		public void DeleteBook(int ReadListID, int BookID);
	}
}
