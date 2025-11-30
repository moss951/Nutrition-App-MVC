using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
 * Note for Andy by Andy:
 * Multiple placeholder classes and variables have been used. Once real objects are available, please replace them:
 * Name of Placeholder : Object It Should Represent
 * UserDbContext : UserDefinedDbContext
 * UserPassDictionary() : User-Password-KV-Dictionary
 * PlaceholderHashFunction : HashFunction
 * User : User
 * GetUser : GetUser
 * UpdateUser : UpdateUser
 * CreateUser: AddUser
 * 
 * There is test code in CompareToHashedPassword
 * 
 */

namespace Nutrition_App.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Services related to logging in, remembering passwords, and registration
        public async Task<bool> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SearchForUser(string username)
        {
            var result = await GetUserByUsername(username);
            return result != null;
        }

        public async Task<IdentityResult> ChangePassword(string username, string currentPassword, string newPassword)
        {
            var user = await GetUserByUsername(username);
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ResetPassword(string username, string newPassword)
        {
            var user = await GetUserByUsername(username);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }


        public async Task<bool> ValidatePasswordRequirements(string password)
        {
            var pv = new PasswordValidator<User>();
            var result = await pv.ValidateAsync(_userManager, new User(), password);
            return result.Succeeded;
        }

        // Basic CRUD
        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            user.BMI = CalculateBMI(user.Weight, user.Height);
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        // Miscellaneous
        public async Task<List<string>> GetUsernames()
        {
            return await _userManager.Users.Select(u => u.UserName).ToListAsync();
        }
        
        public double CalculateBMI(double weight, double height)
        {
            // Assumes that weight is in kg, and height is in metres.
            // BMI is limited to 1 decimal place.
            double bmi = weight / (height * height);
            bmi = Math.Truncate(bmi * 10) / 10;
            return bmi;
        }


    }
}
