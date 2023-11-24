using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		private readonly ICategoryRepository _categoryRepository;
		public BookController(IBookRepository bookRepository,ICategoryRepository categoryRepository ) 
		{ 
			_bookRepository = bookRepository;
			_categoryRepository = categoryRepository;

		}

		#region Get
		[HttpGet("Get All Books")]
		public IActionResult GetAll()
		{
			try
			{
				List<Book> books = _bookRepository.GetAll();
				if (books == null)
				{
					return NotFound($"No Books found.");
				}
				return Ok(books);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}
		[HttpGet("SearchForBook/{Name}")]
		public IActionResult SearchForBook( string Name)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Name))
					return BadRequest("Name cannot be empty");
				var searchResult = _bookRepository.SearchForBooks(Name);
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

		[HttpGet("Get By ID/{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var book = _bookRepository.GetById(id);

				if (book == null)
				{
					return NotFound($"Book with ID {id} not found.");
				}

				return Ok(book);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
		#region Update
		[HttpPut("Update/{id}")]
		public IActionResult Update(int id ,[FromBody]BookDTO book)
		{
			try
			{
				if (ModelState.IsValid)
				{ // for testing dont forget to replace it with name
					var Categ = _categoryRepository.GetById(book.CategoryId);
					if (Categ != null)
					{
						_bookRepository.Update(id, book);
						return Ok("Book is Updated");

					}
					else
						return BadRequest("There is No category with this ID");

				}
				else return BadRequest(ModelState);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}

		#endregion
		#region ADD
		[HttpPost("AddBook")]
		public IActionResult Add([FromBody]BookDTO book)
		{
			try
			{
				if (ModelState.IsValid)
				{ // for testing dont forget to replace it with name
					var Categ = _categoryRepository.GetById(book.CategoryId);
					if (Categ != null)
					{
						_bookRepository.Add(book);
						return Ok("The Book Is Added");
					}
					else
						return BadRequest("There is No category with this ID");

				}
				else return BadRequest(ModelState);
			}
			catch( Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
		#region Delete
		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id) 
		{
			try
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
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
	}
}
