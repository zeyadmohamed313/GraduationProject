using GraduationProject.Serviecs.MyPlanServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
			var myPlan = _myPlanRepository.GetById(id);

			if (myPlan == null)
			{
				return NotFound("MyPlan not found");
			}

			return Ok(myPlan);
		}

		[HttpGet("GetByUserId/{userId}")]
		public IActionResult GetByUserId(string userId)
		{
			var myPlan = _myPlanRepository.GetByUserID(userId);

			if (myPlan == null)
			{
				return NotFound("MyPlan not found");
			}

			return Ok(myPlan);
		}

		[HttpGet("GetAllPlansInMyPlan/{id}")]
		public IActionResult GetAllPlansInMyPlan(int id)
		{
			var plansInMyPlan = _myPlanRepository.GetAllPlansInMyPlan(id);

			if (plansInMyPlan == null || plansInMyPlan.Count == 0)
			{
				return NotFound("No plans found in MyPlan");
			}

			return Ok(plansInMyPlan);
		}

		[HttpPost("AddPlan/{myPlanId}/{planId}")]
		public IActionResult AddPlan(int myPlanId, int planId)
		{
			_myPlanRepository.AddPlan(myPlanId, planId);

			return Ok("Plan added to MyPlanList successfully");
		}

		[HttpDelete("DeletePlan/{myPlanId}/{planId}")]
		public IActionResult DeletePlan(int myPlanId, int planId)
		{
			_myPlanRepository.DeletePlan(myPlanId, planId);

			return Ok("Plan removed from MyPlan successfully");
		}
	}
}
