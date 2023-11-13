using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
			var categories = _categoryRepository.GetAll();
			return Ok(categories);
		}
		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			var category = _categoryRepository.GetById(id);

			if (category == null)
			{
				return NotFound("There is No Category With This ID");
			}

			return Ok(category);
		}
		[HttpGet("All Books From Category/{id}")]
		public IActionResult GetAllBooksFromCategory(int id)
		{
			var books = _categoryRepository.GetAllBooksInSomeCategory(id);
			if (books == null)
				return NotFound("There Is No Books in This Category");
			return Ok(books);
		}
		#endregion
		#region ADD
		[HttpPost("AddCategory")]
		public IActionResult AddCategory([FromBody] CategoryDTO category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_categoryRepository.Add(category);

			return Ok("Category IS Created");
		}

		[HttpPost("Add Book/{BookID}/{CategoryID}")]
		public IActionResult AddBook(int BookID , int CategoryID) 
		{
			var category = _context.Categories.FirstOrDefault(e => e.ID == CategoryID);
			if (category == null)
			{
				return NotFound($"Category with ID {CategoryID} not found.");
			}
			var book = _context.Books.FirstOrDefault(e => e.ID == BookID);
			if (book == null)
			{
				return NotFound($"Book with ID {BookID} not found.");
			}
			_categoryRepository.AddBook(CategoryID, BookID);
			return Ok("Book Is Added");
		}
		#endregion
		#region Update
		[HttpPut("Update/{id}")]
		public IActionResult Update(int id , [FromBody]CategoryDTO category)
		{ 
		    if(ModelState.IsValid)
			{
				_categoryRepository.Update(id, category);
			}
			return BadRequest(ModelState);
		}
		#endregion
		#region Delete
		[HttpDelete("DeleteCategory/{id}")]
		public IActionResult Delete(int id) 
		{ 
		   var TempCategory = _context.Categories.FirstOrDefault(e=>e.ID == id);
			if (TempCategory == null) 
			{
				return NotFound("There Is no Category With This ID");
			}
			_categoryRepository.Delete(id);
			return NoContent();
		}
		[HttpDelete("DeleteBook/{id}")]
		public IActionResult DeleteBook(int id, [FromBody] int BookID)
		{
			var TempCategory = _context.Categories.FirstOrDefault(e => e.ID == id);
			if (TempCategory == null)
			{
				return NotFound("There Is no Category With This ID");
			}
			var TempBook = _context.Books.FirstOrDefault(e => e.ID==BookID);
			if (TempBook == null)
			{
				return NotFound("There Is no Book With This ID");
			}
			_categoryRepository.DeleteBook(id, BookID);
			return NoContent();
		}
		#endregion
	}
}
