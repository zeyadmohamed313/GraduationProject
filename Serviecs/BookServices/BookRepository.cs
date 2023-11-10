using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.BookServices
{
	public class BookRepository:IBookRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public BookRepository(ApplicationContext context)
		{
			_context = context;
		}

		public Book GetById(int id)
		{
			return _context.Books.Find(id);
		}

		public List<Book> GetAll()
		{
			return _context.Books.ToList();
		}

		public void Add(Book book)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book));
			}

			_context.Books.Add(book);
			_context.SaveChanges();
		}

		public void Update(Book book)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book));
			}

			_context.Books.Update(book);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var book = _context.Books.Find(id);
			if (book != null)
			{
				_context.Books.Remove(book);
				_context.SaveChanges();
			}
		}
	}
}
