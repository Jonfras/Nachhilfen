using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Nachhilfen {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Configuration _config;

        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            _config = new Configuration();

            InitializeUi();
            FillComboBox();
        }

        private void InitializeUi() {
            var classes = GetAllClassesFromStudentList();
            CreateCheckBoxes(classes);
            CreateLevelRadioButtons();
            CreateSubjectRadioButtons();
        }


        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            var item = sender as MenuItem;
            var header = item?.Header as string;
            if (header == null) {
                return;
            }

            switch (header) {
                case "Open":
                    Open();
                    break;
                case "Save":
                    Save();
                    break;
                case "Exit":
                    Close();
                    break;
            }
        }

        private void CheckBox_OnChecked(object sender, RoutedEventArgs e) {
            FillComboBox();
        }

        private void CboStudent_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var obj = CboStudent.SelectedItem;
            Student? student = obj as Student;
            if (student == null) {
                return;
            }

            LblStudentName.Content = student.Name;
            ImgStudent.Source = new BitmapImage(
                new Uri(Path.Combine(Environment.CurrentDirectory, @"135a_csv_Images\Images", student.ImagePath)));

            UpdateServiceListBox(student);
            UpdateServiceCount();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e) {
            var obj = CboStudent.SelectedItem;
            Student? student = obj as Student;
            if (student == null) {
                return;
            }

            var levelResult = GetSelectedLevel(out var level);
            var subjectResult = GetSelectedSubject(out var subject);

            if (!levelResult || !subjectResult) {
                return;
            }

            var service = new Service {
                Level = level,
                Subject = subject
            };

            student.Services.Add(service);
            UpdateServiceListBox(student);
            UpdateServiceCount();
        }

        private void Save() {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Text documents (.csv)|*.csv";
            var result = dialog.ShowDialog();
            if (result != true) {
                return;
            }

            var filename = dialog.FileName;
            var lines = GetSerializedTutoringsList();

            File.WriteAllLines(filename, lines);
        }

        private List<string> GetSerializedTutoringsList() {
            List<string> lines = new();
            foreach (var item in CboStudent.Items) {
                if (item is not Student student) {
                    continue;
                }


                foreach (var service in student.Services) {
                    var line = service.Serialize(student.Name);
                    lines.Add(line);
                }
            }

            return lines;
        }

        private void Open() {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Text documents (.csv)|*.csv";
            var result = dialog.ShowDialog();
            if (result != true) {
                return;
            }

            var filename = dialog.FileName;
            var lines = File.ReadAllLines(filename);
            foreach (var line in lines) {
                var serviceResult = Service.TryParse(line, out var service, out var studentName);
                if (!serviceResult) {
                    Console.WriteLine("Service could not be parsed");
                    continue;
                }

                AddServiceToStudent(service!, studentName);
                if (LblStudentName.Content.ToString()!.Equals(studentName)) {
                    UpdateServiceListBox(GetStudentFromName(studentName));
                }

            }
        }

        private void AddServiceToStudent(Service service, string studentName) {
            var student = GetStudentFromName(studentName);
            if (student == null) {
                return;
            }

            student.Services.Add(service);
        }

        private Student? GetStudentFromName(string studentName) {
            var student = _config.Students.FirstOrDefault(s => s.Name.Equals(studentName));
            return student;
        }

        private void UpdateServiceListBox(Student? student) {
            LstServices.Items.Clear();
            if (student == null) {
                return;
            }

            foreach (var studentService in student.Services) {
                LstServices.Items.Add(studentService);
            }
        }

        private void FillComboBox() {
            CboStudent.Items.Clear();
            var selectedClasses = GetSelectedClasses();
            if (selectedClasses.Count == 0) {
                foreach (var student in _config.Students) {
                    CboStudent.Items.Add(student);
                }

                return;
            }

            foreach (var student in _config.Students.Where(student => selectedClasses.Contains(student.Clazz))) {
                CboStudent.Items.Add(student);
            }
        }

        private List<string> GetSelectedClasses() {
            List<string> selectedClasses = new();
            foreach (var child in SpClasses.Children) {
                if (child is CheckBox chk && chk.IsChecked == true) {
                    selectedClasses.Add(chk.Content.ToString()!);
                }
            }

            return selectedClasses;
        }

        private void CreateSubjectRadioButtons() {
            foreach (var subject in _config.Subjects) {
                var rb = new RadioButton {
                    Name = $"rb{subject}",
                    Content = subject,
                };

                SpSubjects.Children.Add(rb);
            }
        }

        private void CreateLevelRadioButtons() {
            foreach (var level in _config.Levels) {
                var rb = new RadioButton {
                    Name = $"rb{level}",
                    Content = level,
                };

                SpLevels.Children.Add(rb);
            }
        }


        private void CreateCheckBoxes(List<string> classes) {
            foreach (var clazz in classes) {
                var chk = new CheckBox {
                    Name = $"chk{clazz}",
                    Content = clazz,
                };
                chk.Checked += CheckBox_OnChecked;
                chk.Unchecked += CheckBox_OnChecked;

                SpClasses.Children.Add(chk);
            }
        }

        private List<string> GetAllClassesFromStudentList() {
            List<string> classes = new();
            foreach (var student in _config.Students) {
                if (classes.Contains(student.Clazz)) {
                    continue;
                }

                classes.Add(student.Clazz);
            }

            classes.Sort();
            return classes;
        }

        private bool GetSelectedSubject(out string subject) {
            foreach (var child in SpSubjects.Children) {
                if (child is RadioButton rb && rb.IsChecked == true) {
                    subject = rb.Content.ToString()!;
                    return true;
                }
            }

            subject = "";
            return false;
        }

        private bool GetSelectedLevel(out int level) {
            foreach (var child in SpLevels.Children) {
                if (child is RadioButton rb && rb.IsChecked == true) {
                    return int.TryParse(rb.Content.ToString(), out level);
                }
            }

            level = 0;
            return false;
        }

        private void UpdateServiceCount() {
            LblServices.Content = $"{LstServices.Items.Count} Nachhilfen:";
        }
    }
}