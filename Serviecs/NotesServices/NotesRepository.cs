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
		   return new NoteDTO { Id = id , UserId =Note.UserId,BookId=Note.BookId,PageNumber=Note.PageNumber,NoteText=Note.NoteText};
		}

		public List<NoteDTO> GetAllNotesForUser(String UserID)
		{
			List<NoteDTO> notes = _context.Notes.Where(e => e.UserId == UserID)
				.Select(note => new NoteDTO
				{
					Id = note.Id,
					UserId = note.UserId,
					BookId = note.BookId,
					PageNumber = note.PageNumber,
					NoteText = note.NoteText
				}).ToList();
			return notes;
		}
		#endregion
		public void Add(Notes note)
		{
			_context.Notes.Add(note);
			_context.SaveChanges();
		}
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

		public void Delete(int id)
		{
			var note = _context.Notes.FirstOrDefault(o => o.Id == id);
			
				_context.Notes.Remove(note);
				_context.SaveChanges();
			
		}
	}
}
