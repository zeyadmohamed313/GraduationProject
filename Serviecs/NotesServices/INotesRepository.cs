using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.NotesServices
{
	public interface INotesRepository
	{
		NoteDTO GetById(int id);
		List<NoteDTO> GetAllNotesForUser(string USerID);
		void Add(Notes note);
		void UpdateNote(int noteId, NoteDTO updatedNote);
		void Delete(int id);
	}
}
