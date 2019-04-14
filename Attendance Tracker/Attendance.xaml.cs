namespace Attendance_Tracker
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public partial class TakeAttendance : Page
    {
      
        internal MySqlConnection connection = new MySqlConnection(Helper.CnnVal("Attendance"));

      
         Student[] Students;

         SaveAttendance[] saveAttendances;

        
        public TakeAttendance()
        {

            InitializeComponent();
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
                    groupName_CB.Items.Add(groupnames);


                }
                getGroups.Close();
                MySqlCommand subjects = new MySqlCommand();

                subjects.CommandText = "Select subjectName from subjects where teacherId=@teacherId2";

                subjects.Parameters.AddWithValue("@teacherId2", Teacher.signInTeacher.getId());
                subjects.Connection = connection;
                MySqlDataReader getSubjects = subjects.ExecuteReader();
                while (getSubjects.Read())
                {
                  
                    subjectName_CB.Items.Add(getSubjects.GetString("subjectName"));


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

       
        public void loadData()
        {
            String groupName = groupName_CB.Text;
            String subject = subjectName_CB.Text;
            if (!String.IsNullOrEmpty(groupName)&&!String.IsNullOrEmpty(subject))
            {
                students_DG.Visibility = Visibility.Visible;


                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand();

                    cmd.CommandText = "select studentID, studentName,absenceCounter,absenceLevel from student where studentGroup in ( select groupId from theGroup where groupName=@groupName )";
                    cmd.Parameters.AddWithValue("groupName", groupName);
                    cmd.Connection = connection;

                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding");
                    students_DG.DataContext = ds;
                    students_DG.DataContext = ds;

                    MySqlCommand selectGroup = new MySqlCommand();
                    selectGroup.CommandText = "Select * from theGroup where groupName=@groupName";
                    selectGroup.Parameters.AddWithValue("@groupName", groupName);
                    selectGroup.Connection = connection;

                    MySqlDataReader login = selectGroup.ExecuteReader();
                    login.Read();
                    int groupId = login.GetInt16("groupId");

                    Group.selectedGroup.setGroupId(groupId);
                    Group.selectedGroup.setGroupName(groupName);
                    for (int i = 0; i < students_DG.Items.Count; i++)
                    {
                        var item = students_DG.Items[i];

                    }

                }
                catch (Exception ex)
                {
                    students_DG.Visibility = Visibility.Hidden;
                    MessageBox.Show("Please check group name");


                }
                finally
                {
                    connection.Close();
                }

                String s = groupName_CB.Text;
            }
            else
            {
                MessageBox.Show("You must choose a group and subject");
            }
        }

    
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void show_studentBt(object sender, RoutedEventArgs e)
        {
            loadData();
        }

     
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void SaveAttendance_bt_Click(object sender, RoutedEventArgs e)
        {

            saveAttendances = new SaveAttendance[students_DG.Items.Count];
            Students = new Student[students_DG.Items.Count];
            Student allStudent;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            for (int i = 0; i < students_DG.Items.Count; i++)
            {
                allStudent = new Student();
                var item = students_DG.Items[i];
                saveAttendances[i] = new SaveAttendance();
                var mycheckbox = students_DG.Columns[3].GetCellContent(item) as CheckBox;

                saveAttendances[i].setAtt_StudentId(Convert.ToInt32((students_DG.Columns[0].GetCellContent(item) as TextBlock).Text));
                saveAttendances[i].setCounter(Convert.ToInt32((students_DG.Columns[2].GetCellContent(item) as TextBlock).Text));
                saveAttendances[i].setAtt_groupId(Group.selectedGroup.getGroupId());
                saveAttendances[i].setSubjectName(subjectName_CB.Text);
               
                allStudent.setName((students_DG.Columns[1].GetCellContent(item) as TextBlock).Text);
                saveAttendances[i].setAttendanceDate(DateTime.Now.ToString("dd-MM-yyyy"));
                if (saveAttendances[i].getCounter() <= 3)
                {
                    saveAttendances[i].setAbsenceLevel("Low");
                }
                else
                {
                    saveAttendances[i].setAbsenceLevel("High");

                }

                if ((bool)mycheckbox.IsChecked == false)
                {
                    connection.Open();

                    String query = "update student set absenceCounter = absenceCounter + 1 , absenceLevel='" + saveAttendances[i].getAbsenceLevel() + "' where studentID =" + saveAttendances[i].getAtt_StudentId();
                    cmd = new MySqlCommand(query, connection);
                    reader = cmd.ExecuteReader();

                    saveAttendances[i].setAttendanceState(0);

                    connection.Close();

                }


                else
                {
                    saveAttendances[i].setAttendanceState(1);

                }



            }
       
            StringBuilder sCommand = new StringBuilder("insert into attendance (attendanceDate,studentId,groupsId,attendanceState,subjectName) values ");
            try
            {
                using (connection)
                {
                    List<string> Rows = new List<string>();


                    for (int x = 0; x < saveAttendances.Length; x++)
                    {


                        Rows.Add(string.Format("('{0}',{1},{2},{3},'{4}')", 
                            MySqlHelper.EscapeString(saveAttendances[x].getAttendanceDate()),
                            MySqlHelper.EscapeString(saveAttendances[x].getAtt_StudentId().ToString()),
                            MySqlHelper.EscapeString(saveAttendances[x].getAtt_groupId().ToString()), 
                            MySqlHelper.EscapeString(saveAttendances[x].getAttendanceState().ToString()),
                            MySqlHelper.EscapeString(saveAttendances[x].getSubjectName())

                            ));
                    }

                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(";");
                    connection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), connection))
                    {

                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                        connection.Close();
                    }


                }
                loadData();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Nothing to save"+ex);

            }
            finally
            {
                connection.Close();
            }
        }

       
    }
}
