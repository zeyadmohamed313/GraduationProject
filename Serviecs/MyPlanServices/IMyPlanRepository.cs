using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.MyPlanServices
{
	public interface IMyPlanRepository
	{
		MyPlan GetById(int id);
		MyPlan GetByUserID(string  UserID);
		List<PlanDTO> GetAllPlansInMyPlan(string UserID);
		 void AddPlan(string UserID,int planId);
		 void DeletePlan(string UserID, int planId);

	}
}
