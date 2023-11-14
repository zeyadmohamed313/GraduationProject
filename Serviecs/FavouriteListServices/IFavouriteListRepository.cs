using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public interface IFavouriteListRepository
	{
	    FavouriteList GetById(int id);
		public FavouriteList GetByUserId(string userId);
		List<BookDTO> GetAllBooksInMyFavouriteList(int id);
		public void AddFavouriteToUser(FavouriteList favouritelist);
		void AddBook(int FavouriteListID, int BookID);
	    void DeleteBook(int FavouriteListID, int BookID);
	}
}
