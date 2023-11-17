using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public class FavouriteListRepository:IFavouriteListRepository
	{
		private readonly ApplicationContext _context; 

		public FavouriteListRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region Get
		public FavouriteList GetById(int id)
		{
			return _context.FavouriteLists.FirstOrDefault(e => e.Id == id);
		}
		public FavouriteList GetByUserId(string userId)
		{
		    return _context.FavouriteLists.FirstOrDefault(e => e.UserId == userId);	
		}

		public List<BookDTO> GetAllBooksInMyFavouriteList(int id)
		{
			var booksInsomeFavouriteList = _context.FavouriteLists.FirstOrDefault(e => e.Id == id).Books
				.Select(book => new BookDTO
				{
					ID = book.ID,
					Title = book.Title,
					Description = book.Description,
					Author = book.Author,
					GoodReadsUrl = book.GoodReadsUrl,
					CategoryId = book.CategoryId,
				}).ToList();
			return booksInsomeFavouriteList;
		}
		public List<BookDTO> SearchForBooks(string UserID,string Name)
		{
			var Fav = _context.FavouriteLists.FirstOrDefault(e => e.UserId == UserID);

			var matchingBooks = Fav.Books
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
		#region Add
		public void AddFavouriteToUser(FavouriteList favouritelist)
		{
			var Check = _context.FavouriteLists.FirstOrDefault(e => e.UserId == favouritelist.UserId);
			_context.FavouriteLists.Add(favouritelist);
			_context.SaveChanges();
		}
		public void AddBook(int FavouriteListID, int BookID)
		{
			FavouriteList TempFav = _context.FavouriteLists.FirstOrDefault(c => c.Id == FavouriteListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempFav.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void DeleteBook(int FavouriteListID, int BookID)
		{
			FavouriteList TempFav = _context.FavouriteLists.FirstOrDefault(c => c.Id == FavouriteListID);
			Book TempBook = _context.Books.FirstOrDefault(c => c.ID == BookID);
			TempFav.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
