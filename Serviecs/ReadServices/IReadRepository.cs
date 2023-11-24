using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.ReadServices
{
	public interface IReadRepository
	{
		Read GetById(int id);
		public Read GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyReadsList(string UserID);
		public List<BookDTO> SearchForBooks(string UserID, string Name);
		public void AddReadToUser(Read read);
		public void AddBook(string UserID, int BookID);
		public void DeleteBook(string UserID, int BookID);
	}
}
