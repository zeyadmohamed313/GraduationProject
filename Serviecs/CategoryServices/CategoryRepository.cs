using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CategoryServices
{
	public class CategoryRepository:ICategoryRepository
	{
		private readonly ApplicationContext _context; 

		public CategoryRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region Get
		public Category GetById(int id)
		{
			return _context.Categories.FirstOrDefault(e => e.ID == id);
		}

		public List<Category> GetAll()
		{
			return _context.Categories.ToList();
		}
		#endregion
		#region ADD
		public void Add(CategoryDTO category)
		{
			Category TempCategory = new Category();
			TempCategory.ID = category.ID;
			TempCategory.Name = category.Name;
			TempCategory.Description = category.Description;
			_context.Categories.Add(TempCategory);
			_context.SaveChanges();
		}
		public void AddBook(int CategoryID, int BookID) 
		{
			Category TempCategory = _context.Categories.FirstOrDefault(c => c.ID == CategoryID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCategory.Books.Add(TempBook);
			_context.SaveChanges();	
		}
		#endregion
		#region Update
		public void Update(int id,CategoryDTO category)
		{
			Category TempCategory = _context.Categories.FirstOrDefault(e=>e.ID == category.ID);
			TempCategory.ID = category.ID;
			TempCategory.Name = category.Name;
			TempCategory.Description = category.Description;
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void Delete(int id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}
		public void DeleteBook(int CategoryID, int BookID)
		{
			Category TempCategory = _context.Categories.FirstOrDefault(c => c.ID == CategoryID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCategory.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
