using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToReadListController : ControllerBase
	{
		private readonly IToReadRepository _toReadRepository;
		private readonly IBookRepository _bookRepository;

		public ToReadListController(IToReadRepository toReadRepository, IBookRepository bookRepository)
		{
			_toReadRepository = toReadRepository;
			_bookRepository = bookRepository;
		}

		#region Get
		[HttpGet("GetToReadList/{id}")]
		public IActionResult GetByID(int id)
		{
			ToRead tempToRead = _toReadRepository.GetById(id);
			if (tempToRead == null)
			{
				return NotFound("This ToReadList Is Not Exists");
			}
			return Ok(tempToRead);
		}

		[HttpGet("GetAllBooksInMyToReadList/{id}")]
		public IActionResult GetAll(int id)
		{
			var allBooksInMyToReadList = _toReadRepository.GetAllBooksInMyToReadsList(id);
			if (allBooksInMyToReadList == null)
			{
				return NotFound("ToRead List Is Empty");
			}
			return Ok(allBooksInMyToReadList);
		}
		#endregion

		#region Add
		[HttpPost("AddBookToToReadList/{toReadIdlist}/{bookId}")]
		public IActionResult AddBookToToRead(int toReadIdlist, int bookId)
		{
			var toRead = _toReadRepository.GetById(toReadIdlist);
			var book = _bookRepository.GetById(bookId);

			if (toRead == null)
			{
				return NotFound("This ToRead Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_toReadRepository.AddBook(toReadIdlist, bookId);
			return Ok("The Book Is Added To the ToRead List");
		}
		#endregion

		#region Delete
		[HttpDelete("DeleteBookFromToReadList/{toReadListId}/{bookId}")]
		public IActionResult DeleteBookFromToRead(int toReadListId, int bookId)
		{
			var toRead = _toReadRepository.GetById(toReadListId);
			var book = _bookRepository.GetById(bookId);

			if (toRead == null)
			{
				return NotFound("This ToRead Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_toReadRepository.DeleteBook(toReadListId, bookId);
			return Ok("The Book Is Removed From the ToRead List");
		}
		#endregion

	}
}
