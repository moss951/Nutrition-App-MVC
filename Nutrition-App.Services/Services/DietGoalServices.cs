using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public class DietGoalServices : IDietGoalServices
    {
        private readonly DietGoalDbContext _context;
        private readonly UserDbContext _userContext;

        public DietGoalServices(DietGoalDbContext context, UserDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public DietGoal GetDietGoal(int id)
        {
            return _context.DietGoals.FirstOrDefault(d => d.Id == id);
        }

        public List<DietGoal> GetDietGoalByUser(string userId)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.Id == userId);
            return _context.DietGoals.Where(d => d.UserId == user.Id).ToList();
        }

        public DietGoal CreateDietGoal(DietGoal dietGoal)
        {
            _context.DietGoals.Add(dietGoal);
            _context.SaveChanges();
            return dietGoal;
        }

        public void DeleteDietGoal(int id)
        {
            _context.DietGoals.Remove(GetDietGoal(id));
        }

        public DietGoal UpdateDietGoal(DietGoal dietGoal)
        {
            _context.DietGoals.Update(dietGoal);
            _context.SaveChanges();
            return dietGoal;
        }
    }
}
