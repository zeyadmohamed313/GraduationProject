using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public interface IFavouriteListRepository
	{
	    FavouriteList GetById(int id);
		public FavouriteList GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyFavouriteList(string UserID);
		List<BookDTO> SearchForBooks(string UserID, string Name);
		public void AddFavouriteToUser(FavouriteList favouritelist);
		void AddBook(string UserID, int BookID);
	    void DeleteBook(string UserID, int BookID);
	}
}
