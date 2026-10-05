using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text; // Додайте цей using
using Lab3_1.core.Models;

namespace Lab3_1.core.Services
{
    public class PersonFileService
    {
        private const string DateFormat = "dd-MM-yyyy";

        public void SaveToFile(string filePath, IEnumerable<Person> people)
        {
            // Вказуємо Encoding.UTF8 для запису
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (var person in people)
                {
                    if (person is Student s)
                    {
                        writer.WriteLine($"STUDENT;{s.LastName};{s.FirstName};{s.Course};{s.StudentCardNumber};{s.BirthDate.ToString(DateFormat)}");
                    }
                    else if (person is Teacher t)
                    {
                        writer.WriteLine($"TEACHER;{t.LastName};{t.FirstName};{t.Department};{t.BirthDate.ToString(DateFormat)}");
                    }
                    else if (person is Astronaut a)
                    {
                        writer.WriteLine($"ASTRONAUT;{a.LastName};{a.FirstName};{a.SpaceMissions};{a.BirthDate.ToString(DateFormat)}");
                    }
                }
            }
        }

        public List<Person> LoadFromFile(string filePath)
        {
            var people = new List<Person>();

            if (!File.Exists(filePath))
                return people;

            // Вказуємо Encoding.UTF8 для зчитування
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(';');
                    string type = parts[0];

                    switch (type)
                    {
                        case "STUDENT":
                            people.Add(new Student(
                                lastName: parts[1],
                                firstName: parts[2],
                                course: int.Parse(parts[3]),
                                studentCardNumber: parts[4],
                                birthDate: DateTime.ParseExact(parts[5], DateFormat, CultureInfo.InvariantCulture)
                            ));
                            break;

                        case "TEACHER":
                            people.Add(new Teacher(
                                lastName: parts[1],
                                firstName: parts[2],
                                birthDate: DateTime.ParseExact(parts[4], DateFormat, CultureInfo.InvariantCulture),
                                department: parts[3]
                            ));
                            break;

                        case "ASTRONAUT":
                            people.Add(new Astronaut(
                                lastName: parts[1],
                                firstName: parts[2],
                                birthDate: DateTime.ParseExact(parts[4], DateFormat, CultureInfo.InvariantCulture),
                                spaceMissions: int.Parse(parts[3])
                            ));
                            break;
                    }
                }
            }

            return people;
        }
    }
}