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
using System.Windows.Shapes;

namespace Attendance_Tracker
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            NametextBlock.Text = Teacher.signInTeacher.getTeacherName();
            TakeAttendance attendance = new TakeAttendance();
            fr.Navigate(attendance);
        }
        private void Addbt_Click(object sender, RoutedEventArgs e)
        {
            AddGroup a = new AddGroup();
             fr.Navigate(a);
          

        }

        private void Attendancebt_Click(object sender, RoutedEventArgs e)
        {
            TakeAttendance attendance = new TakeAttendance();
            fr.Navigate(attendance);
        
        }

        private void Searchbt_Click(object sender, RoutedEventArgs e)
        {
            SearchPage searchPage = new SearchPage();
            fr.Navigate(searchPage);
        }

        private void Button_Copy2_Click(object sender, RoutedEventArgs e)
        {
           Teacher.signInTeacher = new Teacher();
            Teacher.signInTeacher.getId();
            Teacher.signInTeacher.getTeacherName();
            GC.Collect();
          MainWindow signout = new MainWindow(); 
            signout.Show();
            this.Close();
        }
    }
}
