using GraduationProject.Models;

namespace GraduationProject.Serviecs.MyPlanServices
{
	public interface IMyPlanRepository
	{
		MyPlan GetById(int id);
		List<MyPlan> GetAll();
		void Add(MyPlan myPlan);
		void Update(MyPlan myPlan);
		void Delete(int id);
	}
}
