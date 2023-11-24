using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

		public List<BookDTO> GetAllBooksInMyReadsList(string UserID)
		{
			var booksInsomeReadingList = _context.Reads
			.Include(cr => cr.Books)
			.FirstOrDefault(e => e.UserId == UserID)?.Books;
			var bookDTOs = booksInsomeReadingList
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
			var Read = _context.Reads.Include(e=>e.Books).FirstOrDefault(e => e.UserId == UserID);
			var matchingBooks = Read.Books
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
		public void AddReadToUser(Read read)
		{
			var Check = _context.Reads.FirstOrDefault(e => e.UserId == read.UserId);
			_context.Reads.Add(read);
			_context.SaveChanges();
		}
		public void AddBook(string UserID, int BookID)
		{
			Read TempRead = _context.Reads.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
				TempRead.Books.Add(TempBook);
				_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void DeleteBook(string UserID, int BookID)
		{

			Read TempRead = _context.Reads.FirstOrDefault(c => c.UserId == UserID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempRead.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
