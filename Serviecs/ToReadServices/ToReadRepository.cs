using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public class ToReadRepository:IToReadRepository
	{
		private readonly ApplicationContext _context;

		public ToReadRepository(ApplicationContext context)
		{
			_context = context;
		}

		#region Get
		public ToRead GetById(int id)
		{
			return _context.ToReads.FirstOrDefault(e => e.Id == id);
		}
		public ToRead GetByUserId(string userId)
		{
			return _context.ToReads.FirstOrDefault(e => e.UserId == userId);
		}


		public List<BookDTO> GetAllBooksInMyToReadsList(string UserID)
		{
			var booksInsomeToReadingList = _context.Reads
			.Include(cr => cr.Books)
			.FirstOrDefault(e => e.UserId == UserID)?.Books;
			var bookDTOs = booksInsomeToReadingList
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

			return bookDTOs;
		}
		public List<BookDTO> SearchForBooks(string UserID,string Name)
		{
			var ToRead = _context.Reads.Include(e=>e.Books).FirstOrDefault(e => e.UserId == UserID);
			var matchingBooks = ToRead.Books
				.Where(book => book.Title.ToLower().Contains(Name.ToLower()) ||
							   book.Author.ToLower().Contains(Name.ToLower()))
				.Select(book => new BookDTO
				{
					ID = book.ID,
					Title = book.Title,
					Author = book.Author,
					Description = book.Description,
					GoodReadsUrl = book.GoodReadsUrl,
					CategoryId = book.CategoryId,
				})
				.ToList();

			return matchingBooks;
		}

		#endregion

		#region ADD
		public void AddToReadToUser(ToRead toread)
		{
			var Check = _context.ToReads.FirstOrDefault(e => e.UserId == toread.UserId);
			_context.ToReads.Add(toread);
			_context.SaveChanges();
		}
		public void AddBook(string UserID, int BookID)
		{
			ToRead TempToRead = _context.ToReads.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempToRead.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void DeleteBook(string UserID, int BookID)
		{
			ToRead TempToRead = _context.ToReads.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempToRead.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}

