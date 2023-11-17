using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using GraduationProject.Serviecs.ReadServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReadListController : ControllerBase
	{
		private readonly IReadRepository _ReadRepository;
		private readonly IBookRepository _bookRepository;

		public ReadListController(IReadRepository ReadRepository, IBookRepository bookRepository)
		{
			_ReadRepository = ReadRepository;
			_bookRepository = bookRepository;
		}

		#region Get
		[HttpGet("GetReadList/{id}")]
		public IActionResult GetByID(int id)
		{
			Read tempToRead = _ReadRepository.GetById(id);
			if (tempToRead == null)
			{
				return NotFound("This ToReadList Is Not Exists");
			}
			return Ok(tempToRead);
		}
		[HttpGet("GetByUserId/{UserID}")]
		public IActionResult GetByUserID(string UserID)
		{
			var read = _ReadRepository.GetByUserId(UserID);
			if (read == null)
				return NotFound("There is No Currently Reading List for this User");
			return Ok(read);
		}

		[HttpGet("GetAllBooksInMyReadList/{id}")]
		public IActionResult GetAll(int id)
		{
			var allBooksInMyReadList = _ReadRepository.GetAllBooksInMyReadsList(id);
			if (allBooksInMyReadList == null)
			{
				return NotFound("Read List Is Empty");
			}
			return Ok(allBooksInMyReadList);
		}
		[HttpGet("SearchForBook/{Name}")]
		public IActionResult SearchForBook([FromQuery] string Name)
		{

			if (string.IsNullOrWhiteSpace(Name))
				return BadRequest("Name cannot be empty");
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("You should Be Authenticated First");
			}
			var searchResult = _ReadRepository.SearchForBooks(userId, Name);
			if (searchResult == null)
			{
				return NotFound("Book Is Not Found");
			}
			return Ok(searchResult);
		}
		#endregion

		#region Add
		[HttpPost("AddBookToReadList/{ReadIdlist}/{bookId}")]
		public IActionResult AddBookToToRead(int ReadIdlist, int bookId)
		{
			var toRead = _ReadRepository.GetById(ReadIdlist);
			var book = _bookRepository.GetById(bookId);

			if (toRead == null)
			{
				return NotFound("This ToRead Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_ReadRepository.AddBook(ReadIdlist, bookId);
			return Ok("The Book Is Added To the ToRead List");
		}
		#endregion

		#region Delete
		[HttpDelete("DeleteBookFromReadList/{ReadListId}/{bookId}")]
		public IActionResult DeleteBookFromToRead(int ReadListId, int bookId)
		{
			var toRead = _ReadRepository.GetById(ReadListId);
			var book = _bookRepository.GetById(bookId);

			if (toRead == null)
			{
				return NotFound("This ToRead Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_ReadRepository.DeleteBook(ReadListId, bookId);
			return Ok("The Book Is Removed From the ToRead List");
		}
		#endregion
	}
}
