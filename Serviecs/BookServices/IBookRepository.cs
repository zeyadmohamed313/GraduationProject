using GraduationProject.Models;
namespace GraduationProject.Serviecs.BookServices
{
	public interface IBookRepository
	{
		Book GetById(int id);
		List<Book> GetAll();
		void Add(Book book);
		void Update(Book book);
		void Delete(int id);
	}
}
