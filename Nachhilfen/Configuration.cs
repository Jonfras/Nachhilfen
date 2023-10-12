using System;
using System.Collections.Generic;
using System.IO;

namespace Nachhilfen;

public class Configuration {
    public int[] Levels { get; }
    public List<Student> Students { get; set; }
    public List<string> Subjects { get; set; }

    public Configuration() {
        Levels = new int[] { 1, 2, 3, 4, 5 };
        Students = new List<Student>();
        Subjects = new List<string>();
        InitSubjects();
        InitStudents();
    }

    public void InitStudents() {
        var lines = GetFileLines("students.csv");

        foreach (var line in lines) {
            var result = Student.TryParse(line, out var student);
            if (!result) {
                Console.WriteLine("Student could not be parsed");
                continue;
            }
            Students.Add(student);
        }
    }

    public void InitSubjects() {
        var lines = GetFileLines("subjects.csv");
        var split = lines[0].Split(';');
        foreach (var subject in split) {
            if ("".Equals(subject)) {
                continue;
            }
            Subjects.Add(subject);
        }
    }

    private static string[] GetFileLines(string fileName) {
        string path = Path.Combine(Environment.CurrentDirectory, @"135a_csv_Images\csv", fileName);
        Console.WriteLine(path);
        var lines = File.ReadAllLines(path);
        return lines;
    }
}