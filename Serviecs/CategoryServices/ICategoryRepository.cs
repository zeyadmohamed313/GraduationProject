using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CategoryServices
{
	public interface ICategoryRepository
	{
		Category GetById(int id);
		List<Category> GetAll();
		List<BookDTO> GetAllBooksInSomeCategory(int id);
		List<CategoryDTO> SearchForCategory(string name);
		void Add(CategoryDTO category);
		void Update(int id,CategoryDTO category);
		void Delete(int id);
		public void DeleteBook(int CategoryID, int BookID);
	}
}
