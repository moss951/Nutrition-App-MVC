using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nutrition_App.Entities;


namespace Nutrition_App.Services
{
    public interface IUserServices
    {
        // Services related to logging in, remembering passwords, and registration
        public Task<bool> SearchForUser(string username);
        public Task<bool> Login(string username, string password);
        public Task<IdentityResult> ChangePassword(string username, string currentPassword, string newPassword);
        public Task<IdentityResult> ResetPassword(string username, string newPassword);

        // Basic CRUD
        public Task<IdentityResult> CreateUser(User user, string password);
        public Task<User> GetUser(string id);
        public Task<User> GetUserByUsername(string username);

        public Task<List<User>> GetUsers();

        public Task<IdentityResult> UpdateUser(User user);
        public Task<bool> DeleteUser(string id);
        public Task<bool> DeleteUserByUsername(string username);

        // Miscellaneous
        public Task<List<string>> GetUsernames();
        public double CalculateBMI(double height, double weight);
    }
}
