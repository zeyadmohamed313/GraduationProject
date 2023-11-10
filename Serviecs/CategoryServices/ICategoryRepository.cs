using GraduationProject.Models;

namespace GraduationProject.Serviecs.CategoryServices
{
	public interface ICategoryRepository
	{
		Category GetById(int id);
		List<Category> GetAll();
		void Add(Category category);
		void Update(Category category);
		void Delete(int id);
	}
}
