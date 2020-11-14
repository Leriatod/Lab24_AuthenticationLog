using System;
using System.Linq;
using Lab23.Data.Models;
using BC = BCrypt.Net.BCrypt;

namespace Lab23.Services
{
    public class AuthenticationService
    {
        const int MINUES_TO_BLOCK = 3;
        const int MAX_REQUESTS = 7;

        public int TotalRequestCount { get; private set; }
        public DateTime DateTillUsersBlocked { get; private set; }

        private readonly AuthContext _context;
        public AuthenticationService(AuthContext context)
        {
            this._context = context;
            TotalRequestCount = context.TotalRequests.First().TotalRequestCount;
            DateTillUsersBlocked = context.TotalRequests.First().DateTillUsersBlocked;
        }

        public void AuthorizeUser(string userName, string password)
        {
            string hash = BC.HashPassword(password, BC.GenerateSalt());;

            var user = new User 
            { 
                Name = userName, 
                Hash = hash,
                LastLoggedInDate = DateTime.Now
            };

            _context.Add(user);

            updateRequestCount();
            updateDateTillUsersBlockedCount();

            _context.SaveChanges();
        }

        public bool UserExists(string userName)
        {
            updateRequestCount();
            updateDateTillUsersBlockedCount();

            _context.SaveChanges();

            bool hasUserName = _context.Users.Any(u => u.Name == userName);       
            return hasUserName;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Name == userName);
            bool isAuthenticationSuccessful = user != null && BC.Verify(password, user.Hash);
            if (isAuthenticationSuccessful) // update last loggedIn time 
            {
                user.LastLoggedInDate = DateTime.Now;
            }

            updateRequestCount();
            updateDateTillUsersBlockedCount();

            _context.SaveChanges();

            return isAuthenticationSuccessful;
        }

        private void updateRequestCount()
        {
            TotalRequestCount++;
            _context.TotalRequests.First().TotalRequestCount = TotalRequestCount;
        }

        private void updateDateTillUsersBlockedCount()
        {
            if (TotalRequestCount >= MAX_REQUESTS)
            {
                DateTillUsersBlocked = DateTime.Now.AddMinutes(MINUES_TO_BLOCK);
                _context.TotalRequests.First().DateTillUsersBlocked = DateTillUsersBlocked;
                TotalRequestCount = 0;
            }
        }

    }
}