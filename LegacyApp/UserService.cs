using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class UserService
    {
        /// <summary>
        /// This method used for Save user
        /// Modified by Nuwan on 26/01/2024
        /// </summary>
        public bool AddUser(string firstName, string surName, string email, DateTime dateOfBirth, int clientId)
        {
            try
            {
                var clientRepository = new ClientRepository();
                var client = clientRepository.GetById(clientId);

                if (client != null)
                {
                    var user = CreateUserObject(client, firstName, surName, email, dateOfBirth);

                    //Validate required fields using DataAnnotations
                    List<ValidationResult>? errors = user.IsUserValid(user);
                    if (errors != null)
                    {
                        user.ShowErrors(errors);
                        return false;
                    }

                    //Validate user age
                    if (user.CheckAge(user).Equals(false)) { return false; }

                    //Check user credit details
                    CheckAndSetUserCreditLimit(user);

                    if (user.HasCreditLimit && user.CreditLimit < 500) { return false; }

                    UserDataAccess.AddUser(user);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This method used for create user object from input parameters
        /// Created by Nuwan on 26/01/2024
        /// </summary>
        private User CreateUserObject(Client client, string firstName, string surName, string email, DateTime dateOfBirth)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstName,
                Surname = surName
            };
        }

        /// <summary>
        /// his method used for Check user credit details
        /// Created by Nuwan on 26/01/2024
        /// </summary>
        public User CheckAndSetUserCreditLimit(User user)
        {
            if (user.Client.Name == "VeryImportantClient")
            {
                // Skip credit check
                user.HasCreditLimit = false;
            }
            else
            {
                // Do credit check
                user.HasCreditLimit = true;

                using (var userCreditService = new UserCreditServiceClient())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);

                    if (user.Client.Name == "ImportantClient")
                    {
                        // Double credit limit for ImportantClient
                        creditLimit *= 2;
                    }

                    user.CreditLimit = creditLimit;
                }
            }

            return user;
        }

    }
}
