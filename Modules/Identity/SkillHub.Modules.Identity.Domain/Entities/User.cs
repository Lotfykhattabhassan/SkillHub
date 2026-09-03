using System;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; private set; } = string.Empty;   
        public string Phone { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public UserType UserType { get; private set; }
        public UserCredential UserCredential { get; private set; }

        private User() { }
        private User(Guid id, string firstName, string lastName,
            string email, string phone, DateTime dateOfBirth,UserType userType) 
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            FirstName = firstName;

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            LastName = lastName;

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Email must be a valid email address.", nameof(email));
            Email = email;

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.", nameof(phone));
            if (!phone.All(char.IsDigit))
                throw new ArgumentException("Phone Must be only digits.", nameof(phone));
            Phone = phone;

            if (DateTime.UtcNow <= dateOfBirth)
                throw new ArgumentException("Date of Birth cannot be today or in the future", nameof(dateOfBirth));
            if (dateOfBirth.AddYears(18) > DateTime.UtcNow)
                throw new ArgumentException("You should be 18 or bigger.", nameof(dateOfBirth));

            DateOfBirth = dateOfBirth;

            UserType = userType;
        }
        public static User Create(
            string firstName,
            string lastName,
            string email,
            string phone,
            DateTime dateOfBirth,
            UserType userType)
        {
            return new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                phone,
                dateOfBirth,
                userType);
        }
        public void ChangeName(string firstName, string lastName)
        {
            if ( string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            FirstName = firstName;

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            LastName = lastName;
            MarkAsUpdated();
        }
        public void ChangeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Email must be a valid email address.", nameof(email));
            Email = email;
            MarkAsUpdated();
        }
        public void ChangePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.", nameof(phone));
            if (!phone.All(char.IsDigit))
                throw new ArgumentException("Phone Must be only digits.", nameof(phone));
            Phone = phone;
            MarkAsUpdated();
        }
        public void ChangeDateOfBirth(DateTime dateOfBirth)
        {
            if ( DateTime.UtcNow <= dateOfBirth )
                throw new ArgumentException("Date of Birth cannot be today or in the future", nameof(dateOfBirth));
            if ( dateOfBirth.AddYears(18) > DateTime.UtcNow )
                throw new ArgumentException("You should be 18 or bigger.", nameof(dateOfBirth));

            DateOfBirth = dateOfBirth;
            MarkAsUpdated();
        }
        
    }
}
