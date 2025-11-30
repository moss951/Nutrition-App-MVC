using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Data;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public class DietLogServices : IDietLogServices
    {
        private readonly DietLogDbContext _context;
        private readonly UserDbContext _userContext;

        public DietLogServices(DietLogDbContext context, UserDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public DietLog GetDietLog(int id)
        {
            return _context.DietLogs.FirstOrDefault(d => d.Id == id);
        }

        public List<DietLog> GetDietLogsByUser(string userId)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.Id == userId);
            return _context.DietLogs.Where(d => d.UserId == user.Id).OrderByDescending(d => d.DateEaten).ToList();
        }

        public DietLog CreateDietLog(DietLog dietLog)
        {
            _context.DietLogs.Add(dietLog);
            _context.SaveChanges();
            return dietLog;
        }

        public void DeleteDietLog(int id)
        {
            _context.DietLogs.Remove(GetDietLog(id));
        }
        public DietLog UpdateDietLog(DietLog dietLog)
        {
            _context.DietLogs.Update(dietLog);
            _context.SaveChanges();
            return dietLog;
        }
    }
}
