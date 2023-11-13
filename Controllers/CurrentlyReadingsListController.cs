using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			CurrentlyReading TempClr = _currentlyReadingRepository.GetById(id);
			if(TempClr == null)
			{
				return NotFound("This CurrentlyReadingsList Is Not Exsists");
			}
			return Ok(TempClr);
		}
		[HttpGet("GetByUserId/{UserID}")]
		public IActionResult GetByUserID(string UserID) 
		{ 
		   var cur=  _currentlyReadingRepository.GetByUserId(UserID);
			if (cur == null)
				return NotFound("There is No Currently Reading List for this User");
			return Ok(cur);
		}
		[HttpGet("Get All Books in My Clr/{id}")]
		public IActionResult GetAll(int id)
		{
			var AllTheBooksInMyCurrentlyList = _currentlyReadingRepository.GetAllBooksInMyCurrentlyReadingList(id);
			if(AllTheBooksInMyCurrentlyList==null)
			{
				return NotFound("Currently Readings List Is Empty");
		    }
			return Ok(AllTheBooksInMyCurrentlyList);
		}
		#endregion
		#region Add
		[HttpPost("AddBookToCurrentlyReading/{CurrentlyReadingId}/{BookID}")]
		public IActionResult AddBookToCurrentlyReading(int CurrentlyReadingID,int BookID)
		{
			var CLR = _currentlyReadingRepository.GetById(CurrentlyReadingID);
			var Book = _bookRepository.GetById(BookID);
			if( CLR == null) 
			{
				return NotFound("This Currently Reading IS Not Found");
			}
			else if( Book == null ) 
			{
				return NotFound("This Book IS Not Found");

			}
			_currentlyReadingRepository.AddBook(CurrentlyReadingID,BookID);
			return Ok("The Book Is Add To the Currently List");
		}
		#endregion
		#region Delete
		[HttpDelete("DeleteBook/{id}")]
		public IActionResult DeleteBookFromCurrentlyReading(int CurrentlyReadingID, int BookID)
		{
			var CLR = _currentlyReadingRepository.GetById(CurrentlyReadingID);
			var Book = _bookRepository.GetById(BookID);
			if (CLR == null)
			{
				return NotFound("This Currently Reading IS Not Found");
			}
			else if (Book == null)
			{
				return NotFound("This Book IS Not Found");

			}
			_currentlyReadingRepository.DeleteBook(CurrentlyReadingID, BookID);
			return Ok("The Book Is Add To the Currently List");
		}
		#endregion
	}
}
