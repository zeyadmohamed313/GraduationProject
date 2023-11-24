using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.FavouriteListServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouriteListController : ControllerBase
	{
		private readonly IFavouriteListRepository _favouriteListRepository;
		private readonly IBookRepository _bookRepository;

		public FavouriteListController(IFavouriteListRepository favouriteListRepository, IBookRepository bookRepository)
		{
			_favouriteListRepository = favouriteListRepository;
			_bookRepository = bookRepository;
		}

		#region Get

		[HttpGet("GetFavouriteList/{id}")]
		public IActionResult GetByID(int id)
		{
			try
			{
				FavouriteList tempFavouriteList = _favouriteListRepository.GetById(id);
				if (tempFavouriteList == null)
				{
					return NotFound("This FavouriteList does not exist.");
				}
				return Ok(tempFavouriteList);
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
				var favList = _favouriteListRepository.GetByUserId(userID);
				if (favList == null)
					return NotFound("There is no Favourite List for this user.");
				return Ok(favList);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetAllBooksInMyFavouriteList")]
		public IActionResult GetAll()
		{
			try
			{
				var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var allBooksInMyFavList = _favouriteListRepository.GetAllBooksInMyFavouriteList(userID);
				if (allBooksInMyFavList == null || allBooksInMyFavList.Count == 0)
				{
					return NotFound("Favourite List is empty.");
				}
				return Ok(allBooksInMyFavList);
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

				var searchResult = _favouriteListRepository.SearchForBooks(userId, Name);

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

		[HttpPost("AddBookToFavouriteList/{bookId}")]
		public IActionResult AddBookToFavouriteList(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _favouriteListRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_favouriteListRepository.AddBook(userId, bookId);
				return Ok("The book is added to the Favourite List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Delete

		[HttpDelete("DeleteBookFromFavouriteList/{bookId}")]
		public IActionResult DeleteBookFromFavouriteList(int bookId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var toRead = _favouriteListRepository.GetByUserId(userId);
				var book = _bookRepository.GetById(bookId);

				if (book == null)
				{
					return NotFound("This book is not found");
				}

				_favouriteListRepository.DeleteBook(userId, bookId);
				return Ok("The book is removed from the Favourite List");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion
	}
}
