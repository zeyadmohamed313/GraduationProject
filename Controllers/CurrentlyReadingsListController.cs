using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CurrentlyReadingsListController : ControllerBase
	{
		private readonly ICurrentlyReadingRepository _currentlyReadingRepository;
		private readonly IBookRepository _bookRepository;
		public CurrentlyReadingsListController(ICurrentlyReadingRepository currentlyReadingRepository
			, IBookRepository bookRepository)
		{
			_currentlyReadingRepository = currentlyReadingRepository;
			_bookRepository = bookRepository;
		}
		#region Get
		[HttpGet("GetReadingList/{id}")]
		public IActionResult GetByID(int id) 
		{
			try
			{
				CurrentlyReading TempClr = _currentlyReadingRepository.GetById(id);
				if (TempClr == null)
				{
					return NotFound("This CurrentlyReadingsList Is Not Exsists");
				}
				return Ok(TempClr);
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
				var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var cur = _currentlyReadingRepository.GetByUserId(UserID);
				if (cur == null)
					return NotFound("There is No Currently Reading List for this User");
				return Ok(cur);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		[HttpGet("Get All Books in My Clr")]
		public IActionResult GetAll()
		{
			try
			{
				var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var AllTheBooksInMyCurrentlyList = _currentlyReadingRepository.GetAllBooksInMyCurrentlyReadingList(UserID);
				if (AllTheBooksInMyCurrentlyList.Count() == 0)
				{
					return NotFound("Currently Readings List Is Empty");
				}
				return Ok(AllTheBooksInMyCurrentlyList);
			}
			catch(Exception ex)
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
					return BadRequest("You should Be Authenticated First");
				}
				var searchResult = _currentlyReadingRepository.SearchForBooks(userId, Name);
				if (searchResult.Count() == 0)
				{
					return NotFound("Book Is Not Found");
				}
				return Ok(searchResult);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
		#region Add
		[HttpPost("AddBookToCurrentlyReading/{BookID}")]
		public IActionResult AddBookToCurrentlyReading(int BookID)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var CLR = _currentlyReadingRepository.GetByUserId(userId);
				var Book = _bookRepository.GetById(BookID);
				if (CLR == null)
				{
					return NotFound("This Currently Reading IS Not Found");
				}
				if (Book == null)
				{
					return NotFound("This Book IS Not Found");

				}
				_currentlyReadingRepository.AddBook(userId, BookID);
				return Ok("The Book Is Add To the Currently List");
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
		#region Delete
		[HttpDelete("DeleteBook/{id}")]
		public IActionResult DeleteBookFromCurrentlyReading(int BookID)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var CLR = _currentlyReadingRepository.GetByUserId(userId);
				var Book = _bookRepository.GetById(BookID);
				if (CLR == null)
				{
					return NotFound("This Currently Reading IS Not Found");
				}
				else if (Book == null)
				{
					return NotFound("This Book IS Not Found");

				}
				_currentlyReadingRepository.DeleteBook(userId, BookID);
				return Ok("The Book Is Add To the Currently List");
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
	}
}
