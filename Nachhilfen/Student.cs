using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nachhilfen;

public class Student {
    public string Clazz { get; set; }
    public string Firstname { get; set; }
    public string ImagePath { get; set; }
    public string Lastname { get; set; }
    public string Name => $"{Lastname} {Firstname}";
    public List<Service> Services { get; set; }

    public Student() {
        Services = new List<Service>();
    }

    public static Student? Parse(string line) {
        var parts = line.Split(';');
        var partsList = parts.ToList();
        if (partsList.Contains("")) {
            return null;
        }

        var student = new Student {
            Clazz = parts[0],
            Firstname = parts[1],
            Lastname = parts[2]
        };
        student.ImagePath = $"{student.Lastname}_{student.Firstname}.jpg";
        return student;
    }

    public override string ToString() {
        return $"{Name} - Class: {Clazz}";
    }

    public static bool TryParse(string line, out Student? student) {
        student = Parse(line);
        return student != null;
    }
}