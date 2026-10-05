using System;

namespace Lab3_1.core.Models
{
    public abstract class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }

        protected Person(string lastName, string firstName, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
        }

        public abstract string GetDetails();
    }
}