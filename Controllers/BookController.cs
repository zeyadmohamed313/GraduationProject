using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		private readonly ApplicationContext _context;
		public BookController(IBookRepository bookRepository, ApplicationContext applicationContext) 
		{ 
			_bookRepository = bookRepository;
			_context = applicationContext;
		}

		#region Get
		[HttpGet("Get All Books")]
		public IActionResult GetAll()
		{
			List<Book> books = _bookRepository.GetAll();
			if (books == null)
			{
				return NotFound($"No Books found.");
			}
			return Ok(books);
		}

		[HttpGet("Get By ID/{id}")]
		public IActionResult GetById(int id)
		{
			var book = _bookRepository.GetById(id);

			if (book == null)
			{
				return NotFound($"Book with ID {id} not found.");
			}

			return Ok(book);
		}
		#endregion
		#region Update
		[HttpPut("Update/{book}")]
		public IActionResult Update(BookDTO book)
		{
			if (ModelState.IsValid)
			{ // for testing dont forget to replace it with name
				if (_context.Categories.Any(c => c.ID == book.CategoryId))
				{
					_bookRepository.Update(book.ID, book);
					return Ok("Book is Updated");

				}
				else
					return BadRequest("There is No category with this ID");

			}
			else return BadRequest(ModelState);
		}

		#endregion
		#region ADD
		[HttpPost("AddBook/{book}")]
		public IActionResult Add(BookDTO book)
		{
			if (ModelState.IsValid)
			{ // for testing dont forget to replace it with name
				if (_context.Categories.Any(c => c.ID == book.CategoryId))
				{
					_bookRepository.Add(book);
					return Ok(book);

				}
				else
					return BadRequest("There is No category with this ID");

			}
			else return BadRequest(ModelState);
		}
		#endregion
		#region Delete
		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id) 
		{
			var book = _bookRepository.GetById(id);

			if (book == null)
			{
				return NotFound($"Book with ID {id} not found.");
			}
			else
			{
				_bookRepository.Delete(book.ID);
				return NoContent();
			}
		}
		#endregion
	}
}
