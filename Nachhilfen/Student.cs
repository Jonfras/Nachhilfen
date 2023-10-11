using System;
using System.Collections.Generic;

namespace Nachhilfen; 

public class Student
{
    public string Clazz { get; set; }
    public string Firstname { get; set; }
    public string ImagePath { get; set; }
    public string Lastname { get; set; }
    private string Name => $"{Firstname} {Lastname}";
    public List<Service> Services { get; set; }

    public Student()
    {
        Services = new List<Service>();
    }

    public static Student Parse(string line)
    {
        var parts = line.Split(';');
        var student = new Student();
        student.Clazz = parts[0];
        student.Firstname = parts[1];
        student.Lastname = parts[2];
        student.ImagePath = $"{student.Lastname}_{student.Firstname}.jpg";
        return student;
    }

    public override string ToString()
    {
        return $"{Name} - Class: {Clazz} image: {ImagePath}";
    }
}
