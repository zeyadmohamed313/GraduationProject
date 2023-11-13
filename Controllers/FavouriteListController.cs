using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.FavouriteListServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
