using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Note for Andy by Andy:
 * Multiple placeholder classes and variables have been used. Once real objects are available, please replace them:
 * Name of Placeholder : Object It Should Represent
 * PlaceholderContext : UserDefinedDbContext
 * _context.placeholderDictionary : User-Password-KV-Dictionary
 * PlaceholderHashFunction : HashFunction
 * PlaceholderUser : User
 * PlaceholderGetUserMethod : GetUser
 * PlaceholderUpdateUserMethod : UpdateUser
 * PlaceholderAddUserMethod: AddUser
 * 
 * There is test code in SearchForUser, CompareToHashedPassword
 * 
 */

namespace Nutrition_App.Services
{
    public class UserServices : IUserServices
    {
        // private readonly PlaceholderContext _context;
        


        public UserServices() { }
        /*
         * public UserServices(PlaceholderContext context) 
         * { 
         *      _context = context;
         * }
         */

        public bool SearchForUser(string username)
        {
            bool foundUsername = true;
            // returns true if the entered username already exists in the database
            /* string searchResult = _context.Usernames.FirstOrDefault(u => u.Username.Equals(username));
             * if(searchResult == null)
             * {
             *      foundUsername = false;
             * }
             * 
             */

            //TEST CODE
            List<string> _testUsernames = new List<string> { "john" };
            Dictionary<string, string> _testPasswords = new Dictionary<string, string>();
            _testPasswords.Add("john", "password");
             string searchResult = _testUsernames.FirstOrDefault(u => u.Equals(username));
             if(searchResult == null)
             {
                  foundUsername = false;
             }

            return foundUsername;
        }

        public bool SearchForPassword(string username, string plaintextPassword)
        {
            return CompareToHashedPassword(username, plaintextPassword);
        }

        public bool ValidateLoginString(string loginString, bool specialCharsAllowed)
        {
            // used for checking if username or password upon registration / setting new password is erroneous
            /*
             * returns true if the entered string passes the following criteria:
             * a) is not null nor empty
             * b) does not contain whitespace
             * c) does not contain special characters (optional, only usernames should not contain special characters)
             */
            bool valid = true;
            bool isNullOrEmpty = string.IsNullOrEmpty(loginString);
            bool containsWhitespace = loginString.Any(char.IsWhiteSpace);
            bool containsSpecialCharacter = loginString.Any(c => !char.IsLetterOrDigit(c));
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

        public bool CompareToHashedPassword(string username, string plaintextPassword)
        {
            string hashedPassword = "";
            bool valid = false;
            /*
            // True condition will run if username exists in username-password KV dictionary.
            // False condition will run if username does not exist in username-password KV dictionary.
            // Likely will need to create logic to create a KV dictionary.
            if (_context.placeholderDictionary.TryGetValue(username, out hashedPassword))
            {
                 valid = PlaceholderHashFunction(plaintextPassword).Equals(hashedPassword);
            }
            else
            {
                 valid = false;
            }
            
             */

            //TEST CODE
            List<string> _testUsernames = new List<string> { "john" };
            Dictionary<string, string> _testPasswords = new Dictionary<string, string>();
            _testPasswords.Add("john", "password");
            if (_testPasswords.TryGetValue(username, out hashedPassword))
            {
                valid = plaintextPassword.Equals(hashedPassword);
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        public void UpdatePassword(string username, string newPassword)
        {
            /*
            PlaceholderUser user = _context.PlaceholderGetUserMethod(username);
            user.Password = PlaceholderHashFunction(newPassword);
            _context.PlaceholderUpdateUserMethod(user);
            */
        }

        public void RegisterUser(string username, string password)
        {
            /*
             PlaceholderUser user = new PlaceholderUser();
            user.Username = username;
            user.Password = PlaceholderHashFunction(password);
            _context.PlaceholderAddUserMethod.Add(user);
             */
        }

        public bool RegisterResetValidation(string username, string password1, string password2, bool isRegistration)
        {
            bool valid = true;
            bool validUsername = ValidateLoginString(username, false);
            bool takenUsername = SearchForUser(username);
            bool validPassword1 = ValidateLoginString(password1, true);
            bool validPassword2 = ValidateLoginString(password2, true);
            if ( validUsername && validPassword1 && validPassword2 && (password1.Equals(password2)) )
            {
                if (isRegistration && takenUsername)
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
