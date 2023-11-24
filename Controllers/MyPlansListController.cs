using GraduationProject.Serviecs.MyPlanServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MyPlansListController : ControllerBase
	{
		private readonly IMyPlanRepository _myPlanRepository;

		public MyPlansListController(IMyPlanRepository myPlanRepository)
		{
			_myPlanRepository = myPlanRepository;
		}

		[HttpGet("GetByID/{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var myPlan = _myPlanRepository.GetById(id);

				if (myPlan == null)
				{
					return NotFound("MyPlan not found");
				}

				return Ok(myPlan);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}

		[HttpGet("GetByUserId")]
		public IActionResult GetByUserId()
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var myPlan = _myPlanRepository.GetByUserID(userId);

				if (myPlan == null)
				{
					return NotFound("MyPlan not found");
				}

				return Ok(myPlan);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}

		[HttpGet("GetAllPlansInMyPlan/{id}")]
		public IActionResult GetAllPlansInMyPlan()
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var plansInMyPlan = _myPlanRepository.GetAllPlansInMyPlan(userId);

				if (plansInMyPlan == null || plansInMyPlan.Count == 0)
				{
					return NotFound("No plans found in MyPlan");
				}

				return Ok(plansInMyPlan);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}

		[HttpPost("AddPlan/{planId}")]
		public IActionResult AddPlan( int planId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				_myPlanRepository.AddPlan(userId, planId);

				return Ok("Plan added to MyPlanList successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}

		[HttpDelete("DeletePlan/{planId}")]
		public IActionResult DeletePlan( int planId)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				_myPlanRepository.DeletePlan(userId, planId);

				return Ok("Plan removed from MyPlan successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}
	}
}
