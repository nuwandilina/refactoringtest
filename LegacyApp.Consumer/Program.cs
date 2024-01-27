using System;

namespace LegacyApp.Consumer
{
    //INSTRUCTION: Do not edit this class
    class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }

        public static void ProveAddUser(string[] args)
        {
            /*
			 *	DO NOT CHANGE THIS FILE AT ALL
        	*/

            var userService = new UserService();
            var addResult = userService.AddUser("New", "User", "newuser@ukcows.com", new DateTime(1990, 1, 1), 1);
            Console.WriteLine("Adding New User was " + (addResult ? "successful" : "unsuccessful"));
        }
    }
}
