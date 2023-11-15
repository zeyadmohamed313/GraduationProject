using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

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


		public List<BookDTO> GetAllBooksInMyToReadsList(int id)
		{
			var booksInsomeCurrentlyReadingList = _context.ToReads.FirstOrDefault(e => e.Id == id).Books
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
		public void AddToReadToUser(ToRead toread)
		{
			var Check = _context.ToReads.FirstOrDefault(e => e.UserId == toread.UserId);
			_context.ToReads.Add(toread);
			_context.SaveChanges();
		}
		public void AddBook(int ToReadsListID, int BookID)
		{
			ToRead TempToRead = _context.ToReads.FirstOrDefault(c => c.Id == ToReadsListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempToRead.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void DeleteBook(int ToReadListID, int BookID)
		{
			ToRead TempToRead = _context.ToReads.FirstOrDefault(c => c.Id == ToReadListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempToRead.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}

