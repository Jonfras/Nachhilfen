using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
            _config.InitStudents();
            _config.InitSubjects();

            InitializeUi();
        }

        private void InitializeUi() {
            var classes = GetAllClassesFromStudentList();
            CreateCheckBoxes(classes);
            CreateLevelRadioButtons();
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


        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void CheckBox_OnChecked(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}