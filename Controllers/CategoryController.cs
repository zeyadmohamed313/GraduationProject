using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly ApplicationContext _context; // replace it with Repository
		public CategoryController(ICategoryRepository categoryRepository, ApplicationContext applicationContext)
		{
			_categoryRepository = categoryRepository;
			_context = applicationContext;
		}
		#region Get
		[HttpGet("Get All")]
		public IActionResult GetCategories()
		{
			try
			{
				var categories = _categoryRepository.GetAll();
				if (categories == null)
				{
					return NotFound("There is No Category With This ID");
				}
				return Ok(categories);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			try
			{
				var category = _categoryRepository.GetById(id);

				if (category == null)
				{
					return NotFound("There is No Category With This ID");
				}


				return Ok(category);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		[HttpGet("All Books From Category/{id}")]
		public IActionResult GetAllBooksFromCategory(int id)
		{
			try
			{
				var books = _categoryRepository.GetAllBooksInSomeCategory(id);
				if (books == null)
					return NotFound("There Is No Books in This Category");
				return Ok(books);
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
				var searchResult = _categoryRepository.SearchForCategory(Name);
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
		#region ADD
		[HttpPost("AddCategory")]
		public IActionResult AddCategory([FromBody] CategoryDTO category)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				_categoryRepository.Add(category);

				return Ok("Category IS Created");
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		
		#endregion
		#region Update
		[HttpPut("Update/{id}")]
		public IActionResult Update(int id , [FromBody]CategoryDTO category)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var toCheck = _categoryRepository.GetById(id);
					if (toCheck == null)
					{
						return NotFound("There Is No Category With This ID");
					}
					_categoryRepository.Update(id, category);
					return Ok("Update Is Done");
				}
				return BadRequest(ModelState.ErrorCount);
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
		#region Delete
		[HttpDelete("DeleteCategory/{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var TempCategory = _context.Categories.FirstOrDefault(e => e.ID == id);
				if (TempCategory == null)
				{
					return NotFound("There Is no Category With This ID");
				}
				_categoryRepository.Delete(id);
				return NoContent();
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		[HttpDelete("DeleteBook/{id}")]
		public IActionResult DeleteBook(int id, [FromBody] int BookID)
		{
			try
			{
				var TempCategory = _context.Categories.FirstOrDefault(e => e.ID == id);
				if (TempCategory == null)
				{
					return NotFound("There Is no Category With This ID");
				}
				var TempBook = _context.Books.FirstOrDefault(e => e.ID == BookID);
				if (TempBook == null)
				{
					return NotFound("There Is no Book With This ID");
				}
				_categoryRepository.DeleteBook(id, BookID);
				return NoContent();
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");

			}
		}
		#endregion
	}
}
