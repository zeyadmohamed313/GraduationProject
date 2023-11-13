using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

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

		public List<BookDTO> GetAllBooksInMyCurrentlyReadingList(int id)
		{
			var booksInsomeCurrentlyReadingList = _context.CurrentlyReadings.FirstOrDefault(e => e.Id == id).Books
				.Select(book => new BookDTO
				{
					ID = book.ID,
					Title = book.Title,
					Description = book.Description,
					Author = book.Author,
					GoodReadsUrl = book.GoodReadsUrl,
					CategoryId = book.CategoryId,
				}).ToList();
			return booksInsomeCurrentlyReadingList;
		}

		#endregion
		#region ADD
		
		public void AddBook(int CurrentlyReadingsListID, int BookID)
		{
			CurrentlyReading TempCLR = _context.CurrentlyReadings.FirstOrDefault(c => c.Id == CurrentlyReadingsListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCLR.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void DeleteBook(int CurrentlyReadingsListID, int BookID)
		{
			CurrentlyReading TempCLR = _context.CurrentlyReadings.FirstOrDefault(c => c.Id == CurrentlyReadingsListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempCLR.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
