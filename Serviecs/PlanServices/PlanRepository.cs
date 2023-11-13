using GraduationProject.Data.Context;
using GraduationProject.Models;
using GraduationProject.DTO;
using System.Numerics;

namespace GraduationProject.Serviecs.PlanServices
{
	public class PlanRepository:IPlanRepository
	{
		private readonly ApplicationContext _context; 

		public PlanRepository(ApplicationContext context)
		{
			_context = context;
		}
		#region GET
		public Plan GetById(int id)
		{
			return _context.Plans.FirstOrDefault(e=>e.Id==id);
		}

		public List<Plan> GetAll()
		{
			return _context.Plans.ToList();
		}

		#endregion
		#region Add
		public void Add(PlanDTO plandto)
		{
			Plan plan = new Plan();
			plan.Id = plandto.Id;
			plan.Name = plandto.Name;
			plan.Description = plandto.Description;
			_context.Plans.Add(plan);
			_context.SaveChanges();
		}
		public void AddBook(int PlanID, int BookID)
		{
			Plan TempPlan = _context.Plans.FirstOrDefault(e => e.Id == PlanID);
			Book TempBook = _context.Books.FirstOrDefault(e => e.ID == BookID);
			TempPlan.Books.Add(TempBook);
			_context.SaveChanges();
		}
		#endregion
		#region Update
		public void Update(int PlanID,PlanDTO plan)
		{
            Plan TempPlan = _context.Plans.FirstOrDefault(e=>e.Id==PlanID);
			TempPlan.Id= plan.Id;
			TempPlan.Name= plan.Name;
			TempPlan.Description= plan.Description;
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void Delete(int PlanID)
		{
			Plan TempPlan = _context.Plans.FirstOrDefault(e => e.Id == PlanID);
			_context.Plans.Remove(TempPlan);
			_context.SaveChanges();
		}

		public void DeleteBook(int PlanID,int BookID) 
		{
			Plan TempPlan = _context.Plans.FirstOrDefault(e => e.Id == PlanID);
			Book TempBook = _context.Books.FirstOrDefault(e=>e.ID == BookID);
			TempPlan.Books.Remove(TempBook);
			_context.SaveChanges();
		}
		#endregion
	}
}
