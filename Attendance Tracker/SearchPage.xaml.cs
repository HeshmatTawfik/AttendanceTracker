using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        MySqlConnection connection = new MySqlConnection(Helper.CnnVal("Attendance"));

        public SearchPage()
        {
            InitializeComponent();
            //customize date format dd-mm-yyyy
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
           

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select groupName from theGroup where teacherId=@teacherId";
                cmd.Parameters.AddWithValue("@teacherId", Teacher.signInTeacher.getId());
                cmd.Connection = connection;
                MySqlDataReader getGroups = cmd.ExecuteReader();
                while (getGroups.Read())
                {
                    String groupnames = getGroups.GetString("groupName");
                    groupName_CBox.Items.Add(groupnames);


                }
                connection.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);

            }

        }
        public void loadStudentNames()
        {
            String studentName = searchByName_tb.Text;
            if (!String.IsNullOrEmpty(studentName))
            {
                try
                {
                    connection.Open();
                    

                    String q= "select studentName,absenceCounter,absenceLevel, groupName from student  inner join theGroup on ( student.studentName like  '%" + studentName+ "%') and theGroup.groupId=student.studentGroup";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = q;
                    cmd.Connection = connection;
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                  
                    adp.Fill(ds, "LoadDataBinding");
                    if (!(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
                    {
                        searchByNameGrid.Visibility = Visibility.Hidden;

                        MessageBox.Show("No result found ");
                    }
                    else{
                        searchByNameGrid.Visibility = Visibility.Visible;
                        SearchByDateGrid.Visibility = Visibility.Hidden;

                        searchByName_DG.DataContext = ds;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                finally
                {
                    connection.Close();

                }

            }
       }

        private void Search_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem currentItem = ((System.Windows.Controls.ComboBoxItem)search_CB.SelectedItem);
            string text = currentItem.ToString();
            String t = search_CB.SelectedItem.ToString();
            if (currentItem.Content.Equals("Date"))
            {
                searchByDateControls_GR.Visibility = Visibility.Visible;
                searchByNameControls_GR.Visibility = Visibility.Hidden;
          
            }
          else  if (currentItem.Content.Equals("Name"))
            {
                searchByDateControls_GR.Visibility = Visibility.Hidden;
                searchByNameControls_GR.Visibility = Visibility.Visible;
            

            }

         

        }

        private void SearchByName_bt_Click(object sender, RoutedEventArgs e)
        {
            loadStudentNames();
        }
        public void LoadByDate(String date,String groupName)
        {
           
            if (!(String.IsNullOrEmpty(date)&& String.IsNullOrEmpty(groupName)))
            {
                try
                {
                    connection.Open();


                    String q = "select attendanceDate,studentName,attendanceState,groupName,subjectName from attendance inner join student on(student.studentID=attendance.studentId) inner join theGroup on(theGroup.groupId=attendance.groupsId) and attendanceDate= @date and groupName= @groupName";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@groupName", groupName);

                    cmd.CommandText = q;
                    cmd.Connection = connection;
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding2");
                    if (!(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
                    {
                        SearchByDateGrid.Visibility = Visibility.Hidden;

                        MessageBox.Show("No result found ");
                    }
                    else
                    {
                        SearchByDateGrid.Visibility = Visibility.Visible;

                        searchByDate_DG.DataContext = ds;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                finally
                {
                    connection.Close();

                }

            }
        }

        private void SearchByDate_Click(object sender, RoutedEventArgs e)
        {
            String date = LocaleDatePicker.Text;
            String groupName = groupName_CBox.Text;


            if (!(String.IsNullOrWhiteSpace(date) && String.IsNullOrWhiteSpace(groupName)))
            {
                LoadByDate(date, groupName);

            }
            else
            {
                MessageBox.Show("choose Date and group name");

            }
        }

        private void Print_BT_Click(object sender, RoutedEventArgs e)
        {
            PrintDG print = new PrintDG();
            print.printDGByName(searchByName_DG, "");
        }

        private void PrintByDate_BT_Click(object sender, RoutedEventArgs e)
        {
            PrintDG print = new PrintDG();
            print.printDG(searchByDate_DG, "search result");
           
        }

        
    }
}
