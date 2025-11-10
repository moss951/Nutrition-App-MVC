using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Services
{
    public class UserServices : IUserServices
    {
        public UserServices() { }

        public bool SearchForUser(string username)
        {
            // returns true if the entered username already exists in the database
            return true;
        }

        public bool SearchForPassword(string username, string password)
        {
            // returns true if the entered username exists in the database, and if the entered password exists in the database
            return true;
        }

        public bool ValidateLoginString(string loginString, bool specialCharsAllowed)
        {
            bool valid = true;
            /*returns true if the entered string passes the following criteria:
             * a) is not null nor empty
             * b) does not contain whitespace
             * c) does not contain special characters (optional, only usernames should not contain special characters)
             */

            bool isNullOrEmpty = string.IsNullOrEmpty(loginString);
            bool containsWhitespace = loginString.Any(char.IsWhiteSpace);
            bool containsSpecialCharacter = loginString.Any(c => !Char.IsLetterOrDigit(c));
            if (!isNullOrEmpty && !containsWhitespace)
            {
                if (!specialCharsAllowed && containsSpecialCharacter)
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }
    }
}
