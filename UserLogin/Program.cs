using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string username;
            string password;

            Console.Write("Enter a username: ");
            username = Console.ReadLine();
            Console.Write("Enter a password: ");
            password = Console.ReadLine();

            LoginValidation loginValidation = new LoginValidation(username, password, handleError);
            User newRequestedUser = null;
            if (loginValidation.ValidateUserInput(ref newRequestedUser))
            {
                Console.WriteLine("User details:");
                Console.WriteLine("\tUsername: " + newRequestedUser.username);
                Console.WriteLine("\tPassword: " + newRequestedUser.password);
                Console.WriteLine("\tFaculty number: " + newRequestedUser.facultyNumber);
            }
            switch (LoginValidation.currentUserRole)
            {
                case UserRoles.ANONYMOUS:
                    Console.WriteLine("Login failed!");
                    break;
                case UserRoles.ADMIN:
                    Console.WriteLine("Admin login successful!");
                    requestAdminAction();
                    break;
                case UserRoles.INSPECTOR:
                    Console.WriteLine("Inspector login successful!");
                    break;
                case UserRoles.PROFESSOR:
                    Console.WriteLine("Professor login successful!");
                    break;
                case UserRoles.STUDENT:
                    Console.WriteLine("Student login successful!");
                    break;
            }

        }

        public static void handleError(string errorMsg)
        {
            Console.WriteLine("An error has occurred:\n\t" + errorMsg);
        }

        private static void showAdminMenu()
        {
            Console.WriteLine("Admin menu:");
            Console.WriteLine("\t0 - Exit");
            Console.WriteLine("\t1 - Change a user's role");
            Console.WriteLine("\t2 - Change a user's activity");
            Console.WriteLine("\t3 - List users");
            Console.WriteLine("\t4 - List log file");
            Console.WriteLine("\t5 - List log for current session");
        }

        private static void requestAdminAction()
        {
            int action = -1;
            do
            {
                showAdminMenu();
                Console.Write("Enter action ID: ");
                int userChoice = Convert.ToInt32(Console.ReadLine());
                if (userChoice < 0 || userChoice > 5)
                {
                    Console.WriteLine("Invalid action ID.");
                    continue;
                }
                action = userChoice;
            }
            while (action == -1);
            performAdminAction(action);
        }

        private static void performAdminAction(int actionId)
        {
            switch(actionId)
            {
                case 0:
                    return;
                case 1:
                    changeUserRole();
                    break;
                case 2:
                    changeUserActivity();
                    break;
                case 4:
                    Logger.listLogFile();
                    break;
                case 5:
                    listLogForCurrentSession();
                    break;
            }
        }

        private static string requestUsername()
        {
            Console.Write("Enter a username: ");
            return Console.ReadLine();
        }

        private static int getMaxValueForRoles()
        {
            return (int) Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Last();
        }

        private static void printRoles()
        {
            Console.WriteLine("Available roles: ");
            for(int i = 0; i <= getMaxValueForRoles(); i++)
            {
                Console.WriteLine("\t" + i + " - " + (UserRoles) i);
            }
        }

        private static UserRoles requestRole()
        {

            int role = -1;
            do
            {
                printRoles();
                Console.Write("Enter role ID: ");
                int userChoice = Convert.ToInt32(Console.ReadLine());
                if (userChoice < 0 || userChoice > getMaxValueForRoles())
                {
                    Console.WriteLine("Invalid role ID.");
                    continue;
                }
                role = userChoice;
            }
            while (role == -1);
            return (UserRoles) role;
        }

        private static DateTime requestNewActiveUntil()
        {

            Console.Write("Enter a new date to change user's active until: ");
            DateTime activeUntil = Convert.ToDateTime(Console.ReadLine());
            return activeUntil;
        }

        private static void changeUserRole()
        {
            string username = requestUsername();
            UserRoles role = requestRole();
            UserData.assignUserRole(username, role);
        }

        private static void changeUserActivity()
        {
            string username = requestUsername();
            DateTime activeUntil = requestNewActiveUntil();
            UserData.setUserActiveTo(username, activeUntil);
        }

        public static void listLogForCurrentSession()
        {
            Console.Write(Logger.getCurrentSessionActivities());
        }
    }
}
