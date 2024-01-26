using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Client Client { get; set; }


        public List<ValidationResult>? IsUserValid(User user)
        {
            var errorMessage = new List<ValidationResult>();
            var context = new ValidationContext(user);
            bool returnValue = Validator.TryValidateObject(user, context, errorMessage, true);

            if (returnValue == false) { return errorMessage; }
            return null;
        }

        public bool CheckAge(User user)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.DateOfBirth.Year;
            if (currentDate.Month < user.DateOfBirth.Month || (currentDate.Month == user.DateOfBirth.Month && currentDate.Day < user.DateOfBirth.Day)) { age--; }

            if (age < 21)
            {
                Console.WriteLine("error : Age must greater than 21");
                return false;
            }
            return true;
        }

        public void ShowErrors(List<ValidationResult> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"error : {error}");
            }
        }

    }
}
