using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Services
{
    public interface IUserServices
    {
        public bool SearchForUser(string username);

        public bool SearchForPassword(string username, string password);

        public bool ValidateLoginString(string loginString, bool specialCharsAllowed);

        public bool CompareToHashedPassword(string username, string password);

        public void UpdatePassword(string username, string newPassword);

        public void RegisterUser(string username, string password);

        public bool RegisterResetValidation(string username, string password1, string password2, bool isRegistration);
    }
}
