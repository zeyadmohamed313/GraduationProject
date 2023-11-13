using GraduationProject.DTO;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.PlanServices
{
	public interface IPlanRepository
	{
		Plan GetById(int id);
		List<Plan> GetAll();
		public void Add(PlanDTO plandto);
		public void AddBook(int PlanID, int BookID);
		void Update(int PlanID,PlanDTO plan);
		void Delete(int PlanID);
		public void DeleteBook(int PlanID, int BookID);

	}
}
