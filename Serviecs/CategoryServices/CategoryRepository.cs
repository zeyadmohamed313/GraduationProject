using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
		public List<BookDTO> GetAllBooksInSomeCategory(int id)
		{
			var booksInSomeCategory = _context.Books
				.Where(e => e.CategoryId == id)
				.Select(book => new BookDTO
				{
					ID = book.ID,
					Title = book.Title,
					Description = book.Description,
					Author = book.Author,
					GoodReadsUrl = book.GoodReadsUrl,
					CategoryId = book.CategoryId,
				})
				.ToList();
			return booksInSomeCategory;
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
				// delete all the books in the category before delete it
				_context.Books.RemoveRange(_context.Books.Where(e => e.CategoryId == id));
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}
		public void DeleteBook(int CategoryID, int BookID)
		{
			Category TempCategory = _context.Categories.FirstOrDefault(c => c.ID == CategoryID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCategory.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
