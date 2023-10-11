using System;
using System.Collections.Generic;
using System.IO;

namespace Nachhilfen; 

public class Configuration
{
    public int[] Levels { get; }
    public List<Student> Students { get; set; }
    public List<string> Subjects { get; set; }

    public Configuration()
    {
        Levels = new int[] { 1, 2, 3, 4, 5 };
        Students = new List<Student>();
        Subjects = new List<string>();
    }

    public void InitStudents() {
        var lines = GetFileLines("students.csv");
        
        foreach (var line in lines) {
            var student = Student.Parse(line);
            Students.Add(student);
            Console.WriteLine(student);
        }
    }

    public void InitSubjects()
    {
        // Implement subject initialization logic here
    }

    private static string[] GetFileLines(string fileName) {
        string path = Path.Combine(Environment.CurrentDirectory, @"135a_csv_Images\csv", fileName);
        Console.WriteLine(path);
        var lines = File.ReadAllLines(path);
        return lines;
    }
}