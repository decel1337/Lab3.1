using System;
using System.Collections.Generic;
using System.Linq;
using Lab3_1.core.Abstractions;
using Lab3_1.core.Models;
using Lab3_1.core.Services;

namespace Lab3_1
{
    public class ConsoleMenu
    {
        private readonly PersonFileService _fileService = new PersonFileService();
        private const string DefaultFileName = "people_database.txt";

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
                Console.WriteLine("1. Записати базові тестові дані у файл");
                Console.WriteLine("2. Зчитати та вивести всі записи з файлу");
                Console.WriteLine("3. Виконати розрахунок (Студенти 3-го курсу, які народилися влітку)");
                Console.WriteLine("4. Продемонструвати вміння «Співати» (інтерфейс ISinger)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт меню: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SeedData();
                        break;
                    case "2":
                        DisplayAll();
                        break;
                    case "3":
                        CalculateSummerThirdCourseStudents();
                        break;
                    case "4":
                        DemonstrateSinging();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний ввід. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private void SeedData()
        {
            var people = new List<Person>
            {
                new Student("Шевченко", "Андрій", 3, "KB110022", new DateTime(2004, 7, 12)),  // 3 курс, липень (літо)
                new Student("Бондаренко", "Олена", 3, "KB110023", new DateTime(2004, 6, 5)),   // 3 курс, червень (літо)
                new Student("Коваленко", "Ігор", 3, "KB110024", new DateTime(2004, 11, 28)), // 3 курс, листопад (осінь)
                new Student("Ткаченко", "Марія", 2, "KB220011", new DateTime(2005, 8, 19)),   // 2 курс, серпень (літо)
                new Teacher("Дмитренко", "Віктор", new DateTime(1975, 4, 15), "Інженерія ПЗ"),
                new Astronaut("Каденюк", "Леонід", new DateTime(1951, 1, 28), 1)
            };

            _fileService.SaveToFile(DefaultFileName, people);
            Console.WriteLine($"Дані успішно збережено у файл «{DefaultFileName}».");
        }

        private void DisplayAll()
        {
            var people = _fileService.LoadFromFile(DefaultFileName);
            if (!people.Any())
            {
                Console.WriteLine("Файл порожній або не знайдений.");
                return;
            }

            Console.WriteLine("\nСписок усіх записів:");
            foreach (var p in people)
            {
                Console.WriteLine(p.GetDetails());
            }
        }

        private void CalculateSummerThirdCourseStudents()
        {
            var people = _fileService.LoadFromFile(DefaultFileName);
            var students = people.OfType<Student>().ToList();

            // Літні місяці: 6 (червень), 7 (липень), 8 (серпень)
            var summerThirdCourseStudents = students
                .Where(s => s.Course == 3 && s.BirthDate.Month >= 6 && s.BirthDate.Month <= 8)
                .ToList();

            Console.WriteLine($"\nКількість студентів 3-го курсу, які народилися влітку: {summerThirdCourseStudents.Count}");
            foreach (var s in summerThirdCourseStudents)
            {
                Console.WriteLine($"- {s.LastName} {s.FirstName}, Дата: {s.BirthDate:dd-MM-yyyy}");
            }
        }

        private void DemonstrateSinging()
        {
            var people = _fileService.LoadFromFile(DefaultFileName);
            Console.WriteLine("\nДемонстрація виклику методу Sing() для сутностей з інтерфейсом ISinger:");
            foreach (var p in people)
            {
                if (p is ISinger singer)
                {
                    Console.WriteLine(singer.Sing());
                }
            }
        }
    }
}