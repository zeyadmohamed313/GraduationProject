using GraduationProject.Models;

namespace GraduationProject.Serviecs.PlanServices
{
	public interface IPlanRepository
	{
		Plan GetById(int id);
		List<Plan> GetAll();
		void Add(Plan plan);
		void Update(Plan plan);
		void Delete(int id);
	}
}
