using GraduationProject.Models;

namespace GraduationProject.Serviecs.NotesServices
{
	public interface INotesRepository
	{
		Notes GetById(int id);
		List<Notes> GetAll();
		void Add(Notes note);
		void Update(Notes note);
		void Delete(int id);
	}
}
