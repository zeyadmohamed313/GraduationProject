using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.NotesServices
{
	public class NotesRepository:INotesRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public NotesRepository(ApplicationContext context)
		{
			_context = context;
		}

		public Notes GetById(int id)
		{
			return _context.Notes.Find(id);
		}

		public List<Notes> GetAll()
		{
			return _context.Notes.ToList();
		}

		public void Add(Notes note)
		{
			if (note == null)
			{
				throw new ArgumentNullException(nameof(note));
			}

			_context.Notes.Add(note);
			_context.SaveChanges();
		}

		public void Update(Notes note)
		{
			if (note == null)
			{
				throw new ArgumentNullException(nameof(note));
			}

			_context.Notes.Update(note);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var note = _context.Notes.Find(id);
			if (note != null)
			{
				_context.Notes.Remove(note);
				_context.SaveChanges();
			}
		}
	}
}
