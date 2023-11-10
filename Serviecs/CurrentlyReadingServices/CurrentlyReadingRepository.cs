using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public class CurrentlyReadingRepository:ICurrentlyReadingRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public CurrentlyReadingRepository(ApplicationContext context)
		{
			_context = context;
		}

		public CurrentlyReading GetById(int id)
		{
			return _context.CurrentlyReadings.Find(id);
		}

		public List<CurrentlyReading> GetAll()
		{
			return _context.CurrentlyReadings.ToList();
		}

		public void Add(CurrentlyReading currentlyReading)
		{
			if (currentlyReading == null)
			{
				throw new ArgumentNullException(nameof(currentlyReading));
			}

			_context.CurrentlyReadings.Add(currentlyReading);
			_context.SaveChanges();
		}

		public void Update(CurrentlyReading currentlyReading)
		{
			if (currentlyReading == null)
			{
				throw new ArgumentNullException(nameof(currentlyReading));
			}

			_context.CurrentlyReadings.Update(currentlyReading);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var currentlyReading = _context.CurrentlyReadings.Find(id);
			if (currentlyReading != null)
			{
				_context.CurrentlyReadings.Remove(currentlyReading);
				_context.SaveChanges();
			}
		}
	}
}
