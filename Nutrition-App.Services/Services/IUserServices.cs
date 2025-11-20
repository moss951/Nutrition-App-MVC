using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutrition_App.Entities;


namespace Nutrition_App.Services
{
    public interface IUserServices
    {
        // Services related to logging in, remembering passwords, and registration
        public bool SearchForUser(string username);

        public bool SearchForPassword(string username, string password);

        public bool ValidateLoginString(string loginString, bool specialCharsAllowed);

        public bool CompareToHashedPassword(string username, string password);

        public void UpdatePassword(string username, string newPassword);

        public void RegisterUser(string username, string password);

        public bool RegisterResetValidation(string username, string password1, string password2, bool isRegistration);

        // Basic CRUD
        public User CreateUser(User user);
        public User GetUser(int id);
        public User GetUser(string username);

        public List<User> GetUsers();

        public User UpdateUser(User user);
        public void DeleteUser(int id);
        public void DeleteUser(string username);

        // Miscellaneous
        public List<string> GetUsernames();

        public Dictionary<string, string> UserPassDictionary();
    }
}
