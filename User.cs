using System;

namespace MiniEcoMarket
{
    // Abstract base class to define common properties and methods shared by all users
    public abstract class User
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public User(string username, string email)
        {
            Username = username;
            Email = email;
        }

        // DisplayInfo will be implemented differently by each user type
        public abstract void DisplayInfo();

        // Returns a string representation of the user role
        public abstract string GetRole();
    }
}
