using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static string filePath = "students.txt";

    static void Main()
    {
        int choice = 0;

        do
        {
            Console.WriteLine("\n===== STUDENT SYSTEM =====");
            Console.WriteLine("1. Register Student");
            Console.WriteLine("2. Enroll Student Subjects");
            Console.WriteLine("3. Enter Grades");
            Console.WriteLine("4. Show Grades");
            Console.WriteLine("5. Exit");

            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    RegisterStudent();
                    break;
                case 2:
                    EnrollSubjects();
                    break;
                case 3:
                    EnterGrades();
                    break;
                case 4:
                    ShowGrades();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

        } while (choice != 5);
    }

    // ================= MODELS =================
    class Student
    {
        public string fname { get; set; }
        public string mi { get; set; }
        public string lname { get; set; }
        public string birthdate { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string course { get; set; }
        public int year { get; set; }

        public List<Subject> subjects { get; set; } = new List<Subject>();
    }

    class Subject
    {
        public string subjectName { get; set; }
        public string subjectId { get; set; }
        public string grade { get; set; }
    }

    // ================= FEATURES =================
    static void RegisterStudent()
    {
        List<Student> students;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            students = string.IsNullOrWhiteSpace(json)
                ? new List<Student>()
                : JsonSerializer.Deserialize<List<Student>>(json);
        }
        else
        {
            students = new List<Student>();
        }

        Student student = new Student();

        Console.Write("First Name: ");
        student.fname = Console.ReadLine();

        Console.Write("Middle Initial: ");
        student.mi = Console.ReadLine();

        Console.Write("Last Name: ");
        student.lname = Console.ReadLine();

        Console.Write("Birthdate: ");
        student.birthdate = Console.ReadLine();

        Console.Write("Age: ");
        student.age = int.Parse(Console.ReadLine());

        Console.Write("Address: ");
        student.address = Console.ReadLine();

        Console.Write("Contact Number: ");
        student.contact = Console.ReadLine();

        Console.Write("Course: ");
        student.course = Console.ReadLine();

        Console.Write("Year: ");
        student.year = int.Parse(Console.ReadLine());

        students.Add(student);

        string output = JsonSerializer.Serialize(students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, output);

        Console.WriteLine("Student Registered!");
    }

    static void EnrollSubjects()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No data found!");
            return;
        }

        string json = File.ReadAllText(filePath);
        var students = JsonSerializer.Deserialize<List<Student>>(json);

        Console.Write("Enter Student Last Name: ");
        string lname = Console.ReadLine();

var student = students.Find(s => 
    s.fname.Equals(fname, StringComparison.OrdinalIgnoreCase) &&
    s.lname.Equals(lname, StringComparison.OrdinalIgnoreCase));
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        Subject subject = new Subject();

        Console.Write("Subject Name: ");
        subject.subjectName = Console.ReadLine();

        Console.Write("Subject ID: ");
        subject.subjectId = Console.ReadLine();

        student.subjects.Add(subject);

        string output = JsonSerializer.Serialize(students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, output);

        Console.WriteLine("Subject Enrolled!");
    }

    static void EnterGrades()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No data found!");
            return;
        }

        string json = File.ReadAllText(filePath);
        var students = JsonSerializer.Deserialize<List<Student>>(json);

        Console.Write("Student Last Name: ");
        string lname = Console.ReadLine();

var student = students.Find(s => 
    s.fname.Equals(fname, StringComparison.OrdinalIgnoreCase) &&
    s.lname.Equals(lname, StringComparison.OrdinalIgnoreCase));
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        Console.Write("Subject Name: ");
        string subjectName = Console.ReadLine();

        var subject = student.subjects.Find(s => s.subjectName.Equals(subjectName, StringComparison.OrdinalIgnoreCase));

        if (subject == null)
        {
            Console.WriteLine("Subject not found!");
            return;
        }

        Console.Write("Grade: ");
        subject.grade = Console.ReadLine();

        string output = JsonSerializer.Serialize(students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, output);

        Console.WriteLine("Grade Saved!");
    }

    static void ShowGrades()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No data found!");
            return;
        }

        string json = File.ReadAllText(filePath);
        var students = JsonSerializer.Deserialize<List<Student>>(json);

        Console.Write("Enter Student Last Name: ");
        string lname = Console.ReadLine();

var student = students.Find(s => 
    s.fname.Equals(fname, StringComparison.OrdinalIgnoreCase) &&
    s.lname.Equals(lname, StringComparison.OrdinalIgnoreCase));
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        Console.WriteLine($"\n{student.lname}, {student.fname}");
        Console.WriteLine($"{student.course} - Year {student.year}");

        Console.WriteLine("\nSubjects & Grades:");
        foreach (var sub in student.subjects)
        {
            Console.WriteLine($"{sub.subjectName} ({sub.subjectId}) - {sub.grade ?? "No Grade"}");
        }
    }
}
