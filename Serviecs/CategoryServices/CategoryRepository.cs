using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CategoryServices
{
	public class CategoryRepository:ICategoryRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public CategoryRepository(ApplicationContext context)
		{
			_context = context;
		}

		public Category GetById(int id)
		{
			return _context.Categories.Find(id);
		}

		public List<Category> GetAll()
		{
			return _context.Categories.ToList();
		}

		public void Add(Category category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category));
			}

			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void Update(Category category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category));
			}

			_context.Categories.Update(category);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}
	}
}
