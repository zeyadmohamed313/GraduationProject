using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GraduationProject.Serviecs.BookServices
{
	public class BookRepository:IBookRepository
	{
		private readonly ApplicationContext _context; 

		public BookRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region GET
		public Book GetById(int id)
		{
		    var book =  _context.Books.FirstOrDefault(e => e.ID == id);
			return book;
		}
		
		public List<Book> GetAll()
		{
			return _context.Books.ToList();
		}
		#endregion
		#region ADD
		public void Add(BookDTO book)
		{
			Book Temp = new Book();
			Temp.ID = book.ID;
			Temp.Title = book.Title;
			Temp.Description = book.Description;
			Temp.Author = book.Author;
			Temp.CategoryId= book.CategoryId;
			Temp.GoodReadsUrl = book.GoodReadsUrl;
			_context.Books.Add(Temp);
			_context.Categories.FirstOrDefault(e => e.ID == book.CategoryId).Books.Add(Temp);
			_context.SaveChanges();
		}
		#endregion
		#region Update
		public void Update(int id,BookDTO newbook)
		{
	        var targetbook = _context.Books.FirstOrDefault(e=>e.ID == id);
			targetbook.Title=newbook.Title;
			targetbook.Description=newbook.Description;
			targetbook.Author=newbook.Author;
			// dont forget to check the exsistance of this categry table 
			targetbook.CategoryId=newbook.CategoryId;
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void Delete(int id)
		{
			var targetbook = _context.Books.FirstOrDefault(e => e.ID == id);		
				_context.Books.Remove(targetbook);
				_context.SaveChanges();
			
		}
		#endregion
	}
}
