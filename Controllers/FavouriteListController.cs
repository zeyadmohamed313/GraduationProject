using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.FavouriteListServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
			FavouriteList tempFavouriteList = _favouriteListRepository.GetById(id);
			if (tempFavouriteList == null)
			{
				return NotFound("This FavouriteList Is Not Exists");
			}
			return Ok(tempFavouriteList);
		}
		[HttpGet("GetByUserId/{UserID}")]
		public IActionResult GetByUserID(string UserID)
		{
			var fav = _favouriteListRepository.GetByUserId(UserID);
			if (fav == null)
				return NotFound("There is No Currently Reading List for this User");
			return Ok(fav);
		}
		[HttpGet("GetAllBooksInMyFavouriteList/{id}")]
		public IActionResult GetAll(int id)
		{
			var allBooksInMyFavouriteList = _favouriteListRepository.GetAllBooksInMyFavouriteList(id);
			if (allBooksInMyFavouriteList == null)
			{
				return NotFound("FavouriteList Is Empty");
			}
			return Ok(allBooksInMyFavouriteList);
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
			var searchResult = _favouriteListRepository.SearchForBooks(userId, Name);
			if (searchResult == null)
			{
				return NotFound("Book Is Not Found");
			}
			return Ok(searchResult);
		}
		#endregion

		#region Add
		[HttpPost("AddBookToFavouriteList/{favouriteListId}/{bookId}")]
		public IActionResult AddBookToFavouriteList(int favouriteListId, int bookId)
		{
			var favouriteList = _favouriteListRepository.GetById(favouriteListId);
			var book = _bookRepository.GetById(bookId);

			if (favouriteList == null)
			{
				return NotFound("This FavouriteList Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_favouriteListRepository.AddBook(favouriteListId, bookId);
			return Ok("The Book Is Added To the FavouriteList");
		}
		#endregion

		#region Delete
		[HttpDelete("DeleteBookFromFavouriteList/{favouriteListId}/{bookId}")]
		public IActionResult DeleteBookFromFavouriteList(int favouriteListId, int bookId)
		{
			var favouriteList = _favouriteListRepository.GetById(favouriteListId);
			var book = _bookRepository.GetById(bookId);

			if (favouriteList == null)
			{
				return NotFound("This FavouriteList Is Not Found");
			}
			else if (book == null)
			{
				return NotFound("This Book Is Not Found");
			}

			_favouriteListRepository.DeleteBook(favouriteListId, bookId);
			return Ok("The Book Is Removed From the FavouriteList");
		}
		#endregion

	}
}
