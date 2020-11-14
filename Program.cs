using System;
using Lab23.Data.Models;
using Lab23.Services;
using Lab23.View;

namespace Lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            var context         = new AuthContext();
            var identityService = new UserIdentityService();
            var authService     = new AuthenticationService(context);
            var menu            = new ConsoleMenu(authService, identityService);

            bool continueExecution = menu.Start();
            while (continueExecution) 
            {
                continueExecution = menu.Start();
            }
        }
    }
}
