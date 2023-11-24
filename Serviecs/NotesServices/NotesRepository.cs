using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.NotesServices
{
	public class NotesRepository:INotesRepository
	{
		private readonly ApplicationContext _context; 

		public NotesRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region GET
		public NoteDTO GetById(int id)
		{
		   Notes Note =  _context.Notes.FirstOrDefault(e=>e.Id==id);
		   return new NoteDTO { PageNumber=Note.PageNumber,NoteText=Note.NoteText};
		}

		public List<NoteDTO> GetAllNotesForUser(String UserID,int BookID)
		{
			List<NoteDTO> notes = _context.Notes.Where(e => e.UserId == UserID 
			&& e.BookId==BookID)
				.Select(note => new NoteDTO
				{
					
					PageNumber = note.PageNumber,
					NoteText = note.NoteText
				}).ToList();
			return notes;
		}
		#endregion
		#region Add
		public void Add(Notes note)
		{
			_context.Notes.Add(note);
			_context.SaveChanges();
		}
		#endregion
		#region Update
		// controller to Service DTO
		public void UpdateNote(int noteId, NoteDTO updatedNote)
		{
			// Find the existing note by its ID
			var existingNote = _context.Notes.FirstOrDefault(o => o.Id == noteId);
			// Update properties of the existing note with the new values from the DTO
			existingNote.PageNumber = updatedNote.PageNumber;
			existingNote.NoteText = updatedNote.NoteText;
			_context.SaveChanges();
		}
		#endregion
		#region Delete
		public void Delete(int id)
		{
			var note = _context.Notes.FirstOrDefault(o => o.Id == id);
			
				_context.Notes.Remove(note);
				_context.SaveChanges();
			
		}
		#endregion
	}
}
