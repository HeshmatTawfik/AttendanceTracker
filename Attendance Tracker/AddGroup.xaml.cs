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
using System.Collections;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Attendance_Tracker
{
   
    public partial class AddGroup : Page
    {
        Button b=new Button();
        TextBox addt=new TextBox();
        TextBlock tbl = new TextBlock();
        List<TextBox>  list ;
        TextBox[] l;
        MySqlConnection connection = new MySqlConnection(Helper.CnnVal("Attendance"));
        Group newGroup = new Group();

        public AddGroup()
        {
            InitializeComponent();
            b.Click += new RoutedEventHandler(klk);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addGroup(newGroup_tb.Text);
        //    if (String.IsNullOrWhiteSpace(studentNumber_tb.Text))
         //   {
                StudentNumber();
          //  }
        }
        public void addGroup(String name)
        {
            if (!String.IsNullOrWhiteSpace(newGroup_tb.Text)  )
            {
                int z;
                bool studentNumber = int.TryParse(studentNumber_tb.Text,out z);
                if (!String.IsNullOrWhiteSpace(studentNumber_tb.Text)&&z>0) {
                    String groupName = name.Trim().ToLower();
                    newGroup.setGroupName(groupName);
                    try
                    {

                        connection.Open();
                        MySqlCommand insert = new MySqlCommand("insert into theGroup (groupName,teacherId)values(@name , @teacherId)", connection);
                        insert.Parameters.AddWithValue("@name", newGroup.getGroupName());
                        insert.Parameters.AddWithValue("@teacherId", Teacher.signInTeacher.getId());

                        insert.ExecuteNonQuery();

                        insert.CommandText = "Select  groupId from theGroup  where groupName=@groupname";
                        insert.Parameters.AddWithValue("@groupname", newGroup.getGroupName());

                        insert.Connection = connection;
                        MySqlDataReader add_group = insert.ExecuteReader();
                        connection.Close();
                        connection.Open();
                        int id = (int)insert.ExecuteScalar();
                        newGroup.setGroupId(id);
                        connection.Close();
                        MessageBox.Show("Add students name");
                        sp.Visibility = Visibility.Visible;



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Students number must be at least 1");
                }
            }
            else { MessageBox.Show("Group name cant be empty");
                sp.Visibility = Visibility.Hidden;

            }


        }

        private void StudentNumber()
        {
            Regex regex = new Regex("[^0-9]+");
            if (!string.IsNullOrWhiteSpace(studentNumber_tb.Text)) {
                int z = Convert.ToInt32(studentNumber_tb.Text);
                if (z > 0) {
                    list = new List<TextBox>(z);
                    l = new TextBox[z];
                    for (int i = 0; i < l.Length; i++)
                    {
                        TextBlock text = new TextBlock();

                        l[i] = new TextBox();
                        l[i].Margin = new Thickness(0, 3, 0, 5);
                        text.Text = "Student number " + Convert.ToString(i + 1);

                        sp.Children.Add(text);

                        sp.Children.Add(l[i]);
                    }

                    b.Content = "ok";
                    sp.Children.Add(b);
                    sp.Children.Add(tbl);
                }

            }
           
        }
        private void klk(object sender, RoutedEventArgs e)
        {
            String [] studentNames = new string[l.Length];
            try
            {
     
                MySqlCommand cmd;
                MySqlDataReader reader = null;
                for (int i = 0; i < l.Length; i++)
                {
                    connection.Open();

                    studentNames[i] = l[i].Text;
                    String query = "insert into student (studentName,studentGroup)values('"+ l[i].Text.Trim().ToLower()+"','"+ newGroup.getGroupId()+"')";
                    cmd = new MySqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                  
                    connection.Close();

                }
                sp.Visibility = Visibility.Collapsed;
                MessageBox.Show("Successfully added new Group");
                generateRows.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);

            }


          
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddSubjectBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(AddSubject.Text))
                {
                    connection.Open();
                    MySqlCommand insert = new MySqlCommand("insert into subjects (subjectName,teacherId)values(@name , @teacherId)", connection);
                    insert.Parameters.AddWithValue("@name", AddSubject.Text);
                    insert.Parameters.AddWithValue("@teacherId", Teacher.signInTeacher.getId());
                
                    insert.ExecuteNonQuery();
                    AddSubject.Text = "";
                    MessageBox.Show("Subject was added successfully");



                }
                else
                {
                    MessageBox.Show("Subject name can't be empty");
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


        }
    }
}