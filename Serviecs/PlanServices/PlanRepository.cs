using GraduationProject.Data.Context;
using GraduationProject.Models;

namespace GraduationProject.Serviecs.PlanServices
{
	public class PlanRepository:IPlanRepository
	{
		private readonly ApplicationContext _context; // Assuming you have an Entity Framework DbContext

		public PlanRepository(ApplicationContext context)
		{
			_context = context;
		}

		public Plan GetById(int id)
		{
			return _context.Plans.Find(id);
		}

		public List<Plan> GetAll()
		{
			return _context.Plans.ToList();
		}

		public void Add(Plan plan)
		{
			if (plan == null)
			{
				throw new ArgumentNullException(nameof(plan));
			}

			_context.Plans.Add(plan);
			_context.SaveChanges();
		}

		public void Update(Plan plan)
		{
			if (plan == null)
			{
				throw new ArgumentNullException(nameof(plan));
			}

			_context.Plans.Update(plan);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var plan = _context.Plans.Find(id);
			if (plan != null)
			{
				_context.Plans.Remove(plan);
				_context.SaveChanges();
			}
		}
	}
}
