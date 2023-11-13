using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.ReadServices
{
	public class ReadRepository:IReadRepository
	{
		private readonly ApplicationContext _context;

		public ReadRepository(ApplicationContext context)
		{
			_context = context;
		}

		#region Get
		public Read GetById(int id)
		{
			return _context.Reads.FirstOrDefault(e => e.Id == id);
		}
		public Read GetByUserId(string userId)
		{
			return _context.Reads.FirstOrDefault(e => e.UserId==userId);
		}

		public List<BookDTO> GetAllBooksInMyReadsList(int id)
		{
			var booksInsomeCurrentlyReadingList = _context.Reads.FirstOrDefault(e => e.Id == id).Books
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

		public void AddBook(int ReadsListID, int BookID)
		{
			Read TempRead = _context.Reads.FirstOrDefault(c => c.Id == ReadsListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempRead.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void DeleteBook(int ReadListID, int BookID)
		{

			Read TempRead = _context.Reads.FirstOrDefault(c => c.Id == ReadListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempRead.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
