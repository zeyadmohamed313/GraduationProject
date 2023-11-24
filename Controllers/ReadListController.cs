using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
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
			try
			{
				Read tempRead = _ReadRepository.GetById(id);
				if (tempRead == null)
				{
					return NotFound("This ReadList does not exist.");
				}
				return Ok(tempRead);
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
				var Read = _ReadRepository.GetByUserId(userID);
				if (Read == null)
					return NotFound("There is no Read List for this user.");
				return Ok(Read);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetAllBooksInMyReadList")]
		public IActionResult GetAll()
		{
			try
			{
				var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var allBooksInMyReadList = _ReadRepository.GetAllBooksInMyReadsList(userID);
				if (allBooksInMyReadList == null || allBooksInMyReadList.Count == 0)
				{
					return NotFound("Read List is empty.");
				}
				return Ok(allBooksInMyReadList);
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

				var searchResult = _ReadRepository.SearchForBooks(userId, Name);

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

		[HttpPost("AddBookToReadList/{bookId}")]
		public IActionResult AddBookToReadList(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _ReadRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_ReadRepository.AddBook(userId, bookId);
				return Ok("The book is added to the Read List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Delete

		[HttpDelete("DeleteBookFromReadList/{bookId}")]
		public IActionResult DeleteBookFromReadList(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _ReadRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_ReadRepository.DeleteBook(userId, bookId);
				return Ok("The book is removed from the Read List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion
	}
}
