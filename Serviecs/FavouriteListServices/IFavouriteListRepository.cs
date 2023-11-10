using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public interface IFavouriteListRepository
	{
		FavouriteList GetById(int id);
		List<FavouriteList> GetAll();
		void Add(FavouriteList favoriteList);
		void Update(FavouriteList favoriteList);
		void Delete(int id);
	}
}
