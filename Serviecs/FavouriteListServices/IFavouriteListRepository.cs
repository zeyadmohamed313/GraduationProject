using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public interface IFavouriteListRepository
	{
	    FavouriteList GetById(int id);
	    List<BookDTO> GetAllBooksInMyFavouriteList(int id);
	    void AddBook(int FavouriteListID, int BookID);
	    void DeleteBook(int FavouriteListID, int BookID);
	}
}
