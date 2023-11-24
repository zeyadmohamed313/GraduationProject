using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
//using GraduationProject.Serviecs.ToReadServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
			try
			{
				ToRead tempToRead = _toReadRepository.GetById(id);
				if (tempToRead == null)
				{
					return NotFound("This ToReadList does not exist.");
				}
				return Ok(tempToRead);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetByUserId")]
		public IActionResult GetByUserID()
		{
			try
			{
				var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _toReadRepository.GetByUserId(userID);
				if (toRead == null)
					return NotFound("There is no ToRead List for this user.");
				return Ok(toRead);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetAllBooksInMyToReadList")]
		public IActionResult GetAll()
		{
			try
			{
				var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var allBooksInMyToReadList = _toReadRepository.GetAllBooksInMyToReadsList(userID);
				if (allBooksInMyToReadList == null || allBooksInMyToReadList.Count == 0)
				{
					return NotFound("ToRead List is empty.");
				}
				return Ok(allBooksInMyToReadList);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("SearchForBook/{Name}")]
		public IActionResult SearchForBook([FromQuery] string Name)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Name))
					return BadRequest("Name cannot be empty");

				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					return BadRequest("You should be authenticated first");
				}

				var searchResult = _toReadRepository.SearchForBooks(userId, Name);

				if (searchResult.Count() == 0)
				{
					return NotFound("Book is not found");
				}

				return Ok(searchResult);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Add

		[HttpPost("AddBookToToReadList/{bookId}")]
		public IActionResult AddBookToToRead(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _toReadRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_toReadRepository.AddBook(userId, bookId);
				return Ok("The book is added to the ToRead List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Delete

		[HttpDelete("DeleteBookFromToReadList/{bookId}")]
		public IActionResult DeleteBookFromToRead(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _toReadRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_toReadRepository.DeleteBook(userId, bookId);
				return Ok("The book is removed from the ToRead List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion
	}
}
