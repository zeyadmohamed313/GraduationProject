using GraduationProject.DTO;
using GraduationProject.Models;
namespace GraduationProject.Serviecs.BookServices
{
	public interface IBookRepository
	{
		Book GetById(int id);
		List<Book> GetAll();
		void Add(BookDTO book);
		void Update(int id,BookDTO newbook);
		void Delete(int id);
	}
}
