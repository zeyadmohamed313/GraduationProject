using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.MyPlanServices
{
	public interface IMyPlanRepository
	{
		MyPlan GetById(int id);
		MyPlan GetByUserID(string  UserID);
		List<PlanDTO> GetAllPlansInMyPlan(int id);
		 void AddPlan(int myPlanId, int planId);
		 void DeletePlan(int myPlanId, int planId);

	}
}
