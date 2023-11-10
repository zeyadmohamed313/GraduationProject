using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.MyPlanServices
{
	public class MyPlanRepository:IMyPlanRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public MyPlanRepository(ApplicationContext context)
		{
			_context = context;
		}

		public MyPlan GetById(int id)
		{
			return _context.MyPlans.Find(id);
		}

		public List<MyPlan> GetAll()
		{
			return _context.MyPlans.ToList();
		}

		public void Add(MyPlan myPlan)
		{
			if (myPlan == null)
			{
				throw new ArgumentNullException(nameof(myPlan));
			}

			_context.MyPlans.Add(myPlan);
			_context.SaveChanges();
		}

		public void Update(MyPlan myPlan)
		{
			if (myPlan == null)
			{
				throw new ArgumentNullException(nameof(myPlan));
			}

			_context.MyPlans.Update(myPlan);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var myPlan = _context.MyPlans.Find(id);
			if (myPlan != null)
			{
				_context.MyPlans.Remove(myPlan);
				_context.SaveChanges();
			}
		}
	}
}
