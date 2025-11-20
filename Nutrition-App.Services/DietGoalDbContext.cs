using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public class DietGoalDbContext : DbContext
    {
        public DbSet<DietGoal> DietGoals { get; set; }
        public DietGoalDbContext(DbContextOptions<DietGoalDbContext> options) : base(options)
        {
        }
    }
}
