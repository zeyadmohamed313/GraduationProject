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
		public PlanController(IPlanRepository planRepository,IBookRepository bookRepository)
		{
			_planRepository = planRepository;
			_bookRepository = bookRepository;
		}
		#region Get
		[HttpGet("Get All Plans")]
		public IActionResult GetAllPlans()
		{
			List<Plan> plans = _planRepository.GetAll();
			if (plans == null)
			{
				return NotFound("This Plan is Empty");
			}
			return Ok(plans);
		}
		[HttpGet("Get Plan By ID/{id}")]
		public IActionResult GetPlanByID(int id)
		{
			Plan plan = _planRepository.GetById(id);
			if(plan== null)
			{
				return NotFound("There Is No Plan With This ID");
			}
			return Ok(plan);
		}
		#endregion
		#region Add
		[HttpPost("AddPlan")]
		public IActionResult Add([FromBody]PlanDTO plan)
		{
			if (ModelState.IsValid)
			{
				_planRepository.Add(plan);
				return Ok("The Plan Is Added");
			}
			else return BadRequest(ModelState);
		}
		[HttpPost("AddBookToPlan/{PlanID}/{BookID}")]

		public IActionResult AddBookToPlan(int PlanID,int BookID)
		{
			 var Plan = _planRepository.GetById(PlanID);
			 var book = _bookRepository.GetById(BookID);
			if (book == null)
				return NotFound("There Is No Book With This ID");
			if(Plan == null)
				return NotFound("There Is No Plan With This ID");
			_planRepository.AddBook(PlanID, BookID);
			return Ok("The Book Is Added to This Plan");
		}
		#endregion
		#region Update
		[HttpPut("Update/{PlanID}")]
		public IActionResult Update(int PlanID,[FromBody] PlanDTO plan)
		{
			var Plan = _planRepository.GetById(PlanID);
			if (Plan == null)
				return NotFound("There Is No Plan With This ID");
			_planRepository.Update(PlanID, plan);
			return Ok(Plan);
		}
		#endregion
		#region Delete
		[HttpDelete("DeletePlan/{id}")]
		public IActionResult Delete(int PlanID) 
		{
			var Plan = _planRepository.GetById(PlanID);
			if (Plan == null)
				return NotFound("There Is No Plan With This ID");
			_planRepository.Delete(PlanID);
			return NoContent();
		}
		[HttpDelete("DeleteBookFromPlan")]
		public IActionResult DeleteBookFromPlan(int PlanID,int BookID)
		{
			var Plan = _planRepository.GetById(PlanID);
			var book = _bookRepository.GetById(BookID);
			if (book == null)
				return NotFound("There Is No Book With This ID");
			if (Plan == null)
				return NotFound("There Is No Plan With This ID");
			_planRepository.DeleteBook(PlanID, BookID);
			return Ok("The Book Is Deleted From This Plan");
		}

		#endregion
	}
}
