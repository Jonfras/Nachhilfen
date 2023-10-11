using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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