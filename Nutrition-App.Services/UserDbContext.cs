using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }
}
