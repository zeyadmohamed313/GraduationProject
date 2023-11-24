using GraduationProject.Data.Context;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Serviecs.MyPlanServices
{
	public class MyPlanRepository:IMyPlanRepository
	{
		private readonly ApplicationContext _context; 
		public MyPlanRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region Get

		public MyPlan GetById(int id)
		{
			return _context.MyPlans.FirstOrDefault(e => e.Id == id);
		}
		public MyPlan GetByUserID(string UserID)
		{
			return _context.MyPlans.FirstOrDefault(e => e.UserId == UserID);
		}

		public List<PlanDTO> GetAllPlansInMyPlan(string UserID)
		{
			var plansInSomeMyPlan = _context.MyPlans.Include(e=>e.Plans)
				.FirstOrDefault(e => e.UserId == UserID)
				.Plans
				.Select(plan => new PlanDTO
				{
					Id = plan.Id,
					Name = plan.Name,
					Description = plan.Description,
					// Include other properties as needed
				})
				.ToList();

			return plansInSomeMyPlan;
		}
		#endregion
		#region Add

		public void AddPlan(string UserID, int planId)
		{
			MyPlan tempMyPlan = _context.MyPlans.FirstOrDefault(c => c.UserId == UserID);
			Plan tempPlan = _context.Plans.FirstOrDefault(c => c.Id == planId);

			tempMyPlan.Plans.Add(tempPlan);
			_context.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeletePlan(string UserID, int planId)
		{
			MyPlan tempMyPlan = _context.MyPlans.FirstOrDefault(c => c.UserId== UserID);
			Plan tempPlan = _context.Plans.FirstOrDefault(c => c.Id == planId);

			tempMyPlan.Plans.Remove(tempPlan);
			_context.SaveChanges();
		}

		#endregion
	}
}
