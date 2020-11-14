using System;
using Lab23.Services;

namespace Lab23.View
{
    public class ConsoleMenu
    {
        const string LOG_IN_OPTION = "1";
        const string AUTHORIZE_OPTION = "2";
        const string EXIT_OPTION = "3";
        const int MIN_PASSWORD_LENGTH = 6;
        const int MIN_SECRET_VALUE_LENGTH = 6;

        private readonly AuthenticationService _authService;
        private readonly UserIdentityService _identityService;

        public ConsoleMenu(AuthenticationService authService, UserIdentityService identityService)
        {
            this._identityService = identityService;
            this._authService = authService;
        }

        public bool Start()
        {
            displayMainMenu();

            bool continueExecution = true;

            Console.Write("Your choice: ");
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case LOG_IN_OPTION:
                    Console.WriteLine();
                    displayLogInMenu();
                    Console.WriteLine();
                    break;
                case AUTHORIZE_OPTION:
                    Console.WriteLine();
                    displayAuthorizeMenu();
                    Console.WriteLine();
                    break;
                case EXIT_OPTION:
                    continueExecution = false;
                    break;
                default:
                    break;
            }

            return continueExecution;
        }

        private void displayLogInMenu()
        {
            if (usersAreBlocked())
            {
                printUsersBlockedMsg();
                return;
            }

            Console.Write("Type your name: ");
            string userName = Console.ReadLine();

            _identityService.SetNewRandomIntegerNumber();
            Console.Write("Type secret value of function for random number {0}: ", _identityService.RandomIntegerNumber);
            string userSecretValue = Console.ReadLine();

            if (userSecretValue.Length != MIN_SECRET_VALUE_LENGTH  
                || !_identityService.VerifyUserValue(userSecretValue, MIN_SECRET_VALUE_LENGTH))
            {
                Console.WriteLine("Your secret is wrong!");
                return;
            }

            Console.Write("Type your password: ");
            string password = Console.ReadLine();

            bool isAuthenticationSuccessful = this._authService.AuthenticateUser(userName, password);
            if (isAuthenticationSuccessful)
            {
                Console.WriteLine("You are logged in as {0}!", userName);
            }
            else
            {
                Console.WriteLine("Invalid name or password!");
            }
        }

        private void displayAuthorizeMenu()
        {
            if (usersAreBlocked())
            {
                printUsersBlockedMsg();
                return;
            }

            Console.Write("Type your name: ");
            string userName = Console.ReadLine();

            bool hasUserName = this._authService.UserExists(userName);
            if (hasUserName)
            {
                Console.WriteLine("There is user with name {0}", userName);
                return;
            }

            Console.Write("Type your password: ");
            string password = Console.ReadLine();

            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                Console.WriteLine("Password length should no less than {0}", MIN_PASSWORD_LENGTH);
                return;
            }

            if (usersAreBlocked())
            {
                printUsersBlockedMsg();
                return;
            }

            this._authService.AuthorizeUser(userName, password);
            Console.WriteLine("Authorization successful!");
        }

        bool usersAreBlocked()
        {
            return DateTime.Now < this._authService.DateTillUsersBlocked;
        }

        void printUsersBlockedMsg()
        {
            Console.WriteLine("All Users are blocked till {0}", this._authService.DateTillUsersBlocked);
        }

        private void displayMainMenu()
        {
            Console.WriteLine("=====AUTHENTICATION LOG=====");
            Console.WriteLine("1-Log In");
            Console.WriteLine("2-Authorize");
            Console.WriteLine("3-Exit");
        }



    }
}