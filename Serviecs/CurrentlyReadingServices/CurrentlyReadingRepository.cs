using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public class CurrentlyReadingRepository:ICurrentlyReadingRepository
	{
		private readonly ApplicationContext _context; 

		public CurrentlyReadingRepository(ApplicationContext context)
		{
			_context = context;
		}

		#region Get
		public CurrentlyReading GetById(int id)
		{
			return _context.CurrentlyReadings.FirstOrDefault(e => e.Id == id);
		}
		public CurrentlyReading GetByUserId(string userId)
		{
			return _context.CurrentlyReadings.FirstOrDefault(e => e.UserId == userId);

		}
		public List<BookDTO> GetAllBooksInMyCurrentlyReadingList(string UserID)
		{
			// eager loading
			var booksInsomeCurrentlyReadingList = _context.CurrentlyReadings
			.Include(cr => cr.Books)
			.FirstOrDefault(e => e.UserId == UserID)?.Books;
			var bookDTOs = booksInsomeCurrentlyReadingList
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
		public List<BookDTO> SearchForBooks(string UserID ,string Name)
		{
			var CLR = _context.CurrentlyReadings.Include(e=>e.Books).FirstOrDefault(e=>e.UserId == UserID);
			var matchingBooks = CLR.Books
				.Where(book => book.Title.ToLower().Contains(Name.ToLower()) ||
							   book.Author.ToLower().Contains(Name.ToLower()))
				.Select(book => new BookDTO
				{
					ID = book.ID,
					Title = book.Title,
					Author = book.Author,
					Description = book.Description,
					GoodReadsUrl= book.GoodReadsUrl,
					CategoryId = book.CategoryId,
				})
				.ToList();

			return matchingBooks;
		}

		#endregion

		#region ADD
		public void AddCurrentlyReadingListToUser(CurrentlyReading currentlyReading)
		{
			var Check = _context.CurrentlyReadings.FirstOrDefault(e => e.UserId == currentlyReading.UserId);
			_context.CurrentlyReadings.Add(currentlyReading);
			_context.SaveChanges();
			
		}

		public void AddBook(string UserID, int BookID)
		{
			CurrentlyReading TempCLR = _context.CurrentlyReadings.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCLR.Books.Add(TempBook);
			var x = TempCLR.Books;
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void DeleteBook(string UserID, int BookID)
		{
			CurrentlyReading TempCLR = _context.CurrentlyReadings.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCLR.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
