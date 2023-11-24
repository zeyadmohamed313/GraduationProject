using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.BookServices;
using GraduationProject.Serviecs.PlanServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlanController : ControllerBase
	{
		private readonly IPlanRepository _planRepository;
		private readonly IBookRepository _bookRepository;

		public PlanController(IPlanRepository planRepository, IBookRepository bookRepository)
		{
			_planRepository = planRepository;
			_bookRepository = bookRepository;
		}

		#region Get

		[HttpGet("Get All Plans")]
		public IActionResult GetAllPlans()
		{
			try
			{
				List<Plan> plans = _planRepository.GetAll();
				if (plans == null || plans.Count == 0)
				{
					return NotFound("There are no plans available.");
				}
				return Ok(plans);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("Get Plan By ID/{id}")]
		public IActionResult GetPlanByID(int id)
		{
			try
			{
				Plan plan = _planRepository.GetById(id);
				if (plan == null)
				{
					return NotFound("There is no plan with this ID.");
				}
				return Ok(plan);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Add

		[HttpPost("AddPlan")]
		public IActionResult Add([FromBody] PlanDTO plan)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_planRepository.Add(plan);
					return Ok("The plan is added.");
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost("AddBookToPlan/{PlanID}/{BookID}")]
		public IActionResult AddBookToPlan(int PlanID, int BookID)
		{
			try
			{
				var plan = _planRepository.GetById(PlanID);
				var book = _bookRepository.GetById(BookID);

				if (book == null)
					return NotFound("There is no book with this ID.");
				if (plan == null)
					return NotFound("There is no plan with this ID.");

				_planRepository.AddBook(PlanID, BookID);
				return Ok("The book is added to this plan.");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Update

		[HttpPut("Update/{PlanID}")]
		public IActionResult Update(int PlanID, [FromBody] PlanDTO plan)
		{
			try
			{
				var Plan = _planRepository.GetById(PlanID);
				if (Plan == null)
					return NotFound("There is no plan with this ID.");

				_planRepository.Update(PlanID, plan);
				return Ok(Plan);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion

		#region Delete

		[HttpDelete("DeletePlan/{PlanID}")]
		public IActionResult Delete(int PlanID)
		{
			try
			{
				var Plan = _planRepository.GetById(PlanID);
				if (Plan == null)
					return NotFound("There is no plan with this ID.");

				_planRepository.Delete(PlanID);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("DeleteBookFromPlan/{PlanID}/{BookID}")]
		public IActionResult DeleteBookFromPlan(int PlanID, int BookID)
		{
			try
			{
				var Plan = _planRepository.GetById(PlanID);
				var book = _bookRepository.GetById(BookID);

				if (book == null)
					return NotFound("There is no book with this ID.");
				if (Plan == null)
					return NotFound("There is no plan with this ID.");

				_planRepository.DeleteBook(PlanID, BookID);
				return Ok("The book is deleted from this plan.");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		#endregion
	}
}
