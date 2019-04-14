using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Tracker
{
    class Teacher

    {
        private String teacherName;
        private String password;
        private int id;
        private String teacherEmail;
        public void setTeacherEmail(String teacherEmail)
        {
            this.teacherEmail = teacherEmail;
        }

        public String getTeacherEmail()
        {
            return this.teacherEmail;
        }

        public void setTeacherName(String teacherName)
        {
            this.teacherName = teacherName;
        }
       
        public String getTeacherName()
        {
            return this.teacherName;
        }
        public void setPassword(String password)
        {
            this.password = password;
        }
        public String getPassword()
        {
            return this.password;
        }
        public void setId(int id)
        {
             this.id = id;
        }
        public int getId()
        {
            return this.id;

        }
     public  static Teacher signInTeacher = new Teacher();
    }
}
