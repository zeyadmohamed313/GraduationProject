using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CategoryServices
{
	public interface ICategoryRepository
	{
		Category GetById(int id);
		List<Category> GetAll();
		void Add(CategoryDTO category);
		void AddBook(int CategoryID,int BookID);
		void Update(int id,CategoryDTO category);
		void Delete(int id);
		public void DeleteBook(int CategoryID, int BookID);
	}
}
