using LegacyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LeagacyApp.Consumer.Test
{
    [TestClass]
    public class UserTest
    {
        /// <summary>
        /// Test for all correct input parameters
        /// </summary>
        [TestMethod]
        public void AddUserReturnsTrue()
        {
            var user = new User();
            var userService = new UserService();

            int clientId = 1;
            string firstName = "Nuwan Dilina";
            string surName = "Hettiarachchi";
            string email = "hnuwandilina@gmail.com";
            var dateOfBirth = new DateTime(1988, 11, 16);

            bool result = userService.AddUser(firstName, surName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test without entering first name
        /// </summary>
        [TestMethod]
        public void AddUserWithoutFirstName()
        {
            var user = new User();
            var userService = new UserService();

            int clientId = 1;
            string firstName = "";
            string surName = "Hettiarachchi";
            string email = "hnuwandilina@gmail.com";
            var dateOfBirth = new DateTime(1988, 11, 16);

            bool result = userService.AddUser(firstName, surName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test without entering last name
        /// </summary>
        [TestMethod]
        public void AddUserWithoutLastName()
        {
            var user = new User();
            var userService = new UserService();

            int clientId = 1;
            string firstName = "Nuwan Dilina";
            string surName = "";
            string email = "hnuwandilina@gmail.com";
            var dateOfBirth = new DateTime(1988, 11, 16);

            bool result = userService.AddUser(firstName, surName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test with invalid email
        /// </summary>
        [TestMethod]
        public void AddUserWithInvalidEmail()
        {
            var user = new User();
            var userService = new UserService();

            int clientId = 1;
            string firstName = "Nuwan Dilina";
            string surName = "Hettiarachchi";
            string email = "hnuwandilinagmail.com";
            var dateOfBirth = new DateTime(1988, 11, 16);

            bool result = userService.AddUser(firstName, surName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }
    }
}
