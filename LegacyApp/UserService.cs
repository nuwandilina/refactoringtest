using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surName, string email, DateTime dateOfBirth, int clientId)
        {
            try
            {
                var clientRepository = new ClientRepository();
                var client = clientRepository.GetById(clientId);

                if (client != null)
                {
                    var user = new User
                    {
                        Client = client,
                        DateOfBirth = dateOfBirth,
                        EmailAddress = email,
                        Firstname = firstName,
                        Surname = surName
                    };

                    List<ValidationResult>? errors = user.IsUserValid(user);
                    if (errors != null)
                    {
                        user.ShowErrors(errors);
                        return false;
                    }

                    if (user.CheckAge(user).Equals(false)) { return false; }

                    if (client.Name == "VeryImportantClient")
                    {
                        // Skip credit check
                        user.HasCreditLimit = false;
                    }
                    else if (client.Name == "ImportantClient")
                    {
                        // Do credit check and double credit limit
                        user.HasCreditLimit = true;
                        using (var userCreditService = new UserCreditServiceClient())
                        {
                            int creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                            creditLimit = creditLimit * 2;
                            user.CreditLimit = creditLimit;
                        }
                    }
                    else
                    {
                        // Do credit check
                        user.HasCreditLimit = true;
                        using (var userCreditService = new UserCreditServiceClient())
                        {
                            int creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                            user.CreditLimit = creditLimit;
                        }
                    }

                    if (user.HasCreditLimit && user.CreditLimit < 500)
                    {
                        return false;
                    }

                    UserDataAccess.AddUser(user);

                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
