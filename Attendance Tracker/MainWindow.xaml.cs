using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using MySql.Data.MySqlClient;


namespace Attendance_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = new MySqlConnection(Helper.CnnVal("Attendance"));
          Teacher teacherAccount = new Teacher();

        public MainWindow()
        {
            InitializeComponent();
        }

    

        private void Signupb_Click(object sender, RoutedEventArgs e)
        {
            signupBoard.Visibility = Visibility.Visible;

        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            //String name = username.Text.Trim().ToLower();
            String pass = password.Password;
            
            try
            {
                connection.Open();
                String user = username.Text.Trim().ToLower();
                String teachPass = password.Password;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from teacher where teacherName=@user and BINARY  password=@pass";
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", teachPass);

                cmd.Connection = connection;
                MySqlDataReader login = cmd.ExecuteReader();

            

                if (login.Read()) {
                    int id = login.GetInt16("teacherId");
                    Teacher.signInTeacher.setTeacherName(username.Text.Trim().ToLower());
                    Teacher.signInTeacher.setId(id);
             

                    HomeWindow homeWindow = new HomeWindow();
                    this.Close();
                    homeWindow.Show();
                    connection.Close();


                }
                else
                {
                    MessageBox.Show("Wrong user name or password");
                    connection.Close();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex);
                connection.Close();

            }





        }

        private void Submitsignupb_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Teacher teacher = new Teacher();
                if(!String.IsNullOrWhiteSpace(SignUpNametb.Text)&& !String.IsNullOrWhiteSpace(signUpPassword.Password) && !String.IsNullOrWhiteSpace(confirmPassword.Password)&& !String.IsNullOrWhiteSpace(SignUpEmail.Text))
                {
                    connection.Open();

                    if (signUpPassword.Password.Equals(confirmPassword.Password))
                    {
                        
                        teacher.setTeacherName(SignUpNametb.Text.Trim().ToLower());
                        teacher.setPassword(signUpPassword.Password.Trim());
                        teacher.setTeacherEmail(SignUpEmail.Text.Trim());
                        if (IsValidEmail(teacher.getTeacherEmail()))
                        {
                            MySqlCommand insert = new MySqlCommand("insert into teacher (teacherName,password,teacherEmail)values(@name , @password,@email)", connection);
                            insert.Parameters.AddWithValue("@name", teacher.getTeacherName());
                            insert.Parameters.AddWithValue("@password", teacher.getPassword());
                            insert.Parameters.AddWithValue("@email", teacher.getTeacherEmail());


                            if (insert.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Successfuly registered");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Email");

                        }

                    }
                    else
                    {
                        MessageBox.Show("Your password and confirmation password do not match");
                    }
                }

                else
                {
                    MessageBox.Show("please fill all fields ");

                }
            }
           
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("duplicate entry"))
                {
                    MessageBox.Show("User already exsit");
                }
                else
                {
                    MessageBox.Show("" + ex);
                }

            }
            finally
            {
                connection.Close();

            }






        }

        private void BackToSignIn_bt_Click(object sender, RoutedEventArgs e)
        {
            signupBoard.Visibility = Visibility.Hidden;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new System.Globalization.IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
