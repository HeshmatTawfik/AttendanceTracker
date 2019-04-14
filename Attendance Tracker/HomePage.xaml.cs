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

namespace Attendance_Tracker
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Addbt_Click(object sender, RoutedEventArgs e)
        {
            AddGroup a = new AddGroup();
            fr.Navigate(a);
          //  MainWindow m = new MainWindow();
          // this.Content = a;
            
        }

        private void Attendancebt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
