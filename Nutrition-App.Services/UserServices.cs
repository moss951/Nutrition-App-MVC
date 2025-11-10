using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return foundUsername;
        }

        public bool SearchForPassword(string username, string plaintextPassword)
        {
            return CompareToHashedPassword(username, plaintextPassword);
        }

        public bool ValidateLoginString(string loginString, bool specialCharsAllowed)
        {

            /*
             * used for checking if username or password upon registration / setting new password is erroneous
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
            Dictionary<string, string> placeholderDictionary = new Dictionary<string, string>();
            string hashedPassword = "";
            bool valid = false;
            /*
            // true condition will run if username exists in username-password KV dictionary.
            // false condition will run if username does not exist in username-password KV dictionary.
            if (_context.placeholderDictionary.TryGetValue(username, out hashedPassword))
            {
                 valid = placeholderHashFunction(plaintextPassword).Equals(hashedPassword);
            }
            else
            {
                 valid = false;
            }
            
             */

            return valid;
        }

    }
}
