using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.FavouriteListServices
{
	public class FavouriteListRepository:IFavouriteListRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public FavouriteListRepository(ApplicationContext context)
		{
			_context = context;
		}

		public FavouriteList GetById(int id)
		{
			return _context.FavouriteLists.Find(id);
		}

		public List<FavouriteList> GetAll()
		{
			return _context.FavouriteLists.ToList();
		}

		public void Add(FavouriteList favouriteList)
		{
			if (favouriteList == null)
			{
				throw new ArgumentNullException(nameof(favouriteList));
			}

			_context.FavouriteLists.Add(favouriteList);
			_context.SaveChanges();
		}

		public void Update(FavouriteList favouriteList)
		{
			if (favouriteList == null)
			{
				throw new ArgumentNullException(nameof(favouriteList));
			}

			_context.FavouriteLists.Update(favouriteList);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var favoriteList = _context.FavouriteLists.Find(id);
			if (favoriteList != null)
			{
				_context.FavouriteLists.Remove(favoriteList);
				_context.SaveChanges();
			}
		}
	}
}
