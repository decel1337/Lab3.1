using System;
using Lab3_1.core.Abstractions;

namespace Lab3_1.core.Models
{
    // Сутність Студент (Варіант 1)
    public class Student : Person, ISinger
    {
        public int Course { get; set; }
        public string StudentCardNumber { get; set; }

        public Student(string lastName, string firstName, int course, string studentCardNumber, DateTime birthDate)
            : base(lastName, firstName, birthDate)
        {
            Course = course;
            StudentCardNumber = studentCardNumber;
        }

        public string Sing() => $"{FirstName} {LastName} співає студентський гімн «Gaudeamus».";

        public override string GetDetails() =>
            $"Студент: {LastName} {FirstName}, Курс: {Course}, Квиток: {StudentCardNumber}, Дата нар.: {BirthDate:dd-MM-yyyy}";
    }

    // Сутність Викладач (Teacher)
    public class Teacher : Person, ISinger
    {
        public string Department { get; set; }

        public Teacher(string lastName, string firstName, DateTime birthDate, string department)
            : base(lastName, firstName, birthDate)
        {
            Department = department;
        }

        public string Sing() => $"{FirstName} {LastName} співає пісню викладачів.";

        public override string GetDetails() =>
            $"Викладач: {LastName} {FirstName}, Кафедра: {Department}, Дата нар.: {BirthDate:dd-MM-yyyy}";
    }

    // Сутність Космонавт (Astronaut)
    public class Astronaut : Person
    {
        public int SpaceMissions { get; set; }

        public Astronaut(string lastName, string firstName, DateTime birthDate, int spaceMissions)
            : base(lastName, firstName, birthDate)
        {
            SpaceMissions = spaceMissions;
        }

        public override string GetDetails() =>
            $"Космонавт: {LastName} {FirstName}, Кількість місій: {SpaceMissions}, Дата нар.: {BirthDate:dd-MM-yyyy}";
    }
}