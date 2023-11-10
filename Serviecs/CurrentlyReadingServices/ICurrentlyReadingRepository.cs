using GraduationProject.Models;

namespace GraduationProject.Serviecs.CurrentlyReadingServices
{
	public interface ICurrentlyReadingRepository
	{
		CurrentlyReading GetById(int id);
		List<CurrentlyReading> GetAll();
		void Add(CurrentlyReading currentlyReading);
		void Update(CurrentlyReading currentlyReading);
		void Delete(int id);
	}
}
