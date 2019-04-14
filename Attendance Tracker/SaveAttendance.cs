using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Tracker
{
    class SaveAttendance
    {
        private int attendanceId;
        private String attendanceDate;
        private int att_StudentId;
        private int att_groupId;
        private int attendanceState;
        private int counter;
        private String absenceLevel;
        private String subjectName;
        public void setSubjectName(String subjectName)
        {
            this.subjectName = subjectName;
        }
        public String getSubjectName()
        {
            return this.subjectName;
        }

        //
        public void setAbsenceLevel(String absenceLevel)
        {
            this.absenceLevel = absenceLevel;
        }
        public String getAbsenceLevel()
        {
            return this.absenceLevel;
        }
        public void setCounter(int counter)
        {
            this.counter = counter;
        }
        public int getCounter()
        {
            return this.counter;
        }

        public void setAttendanceId(int attendanceId)
        {
            this.attendanceId = attendanceId;
        }
        public int getAttendanceId()
        {
            return this.attendanceId;
        }
        public void setAttendanceDate(String attendanceDate)
        {
            this.attendanceDate = attendanceDate;
        }
        public String getAttendanceDate()
        {
            return this.attendanceDate;
        }
        public void setAtt_StudentId(int att_StudentId)
        {
            this.att_StudentId = att_StudentId;
        }
              public int getAtt_StudentId()
        {
            return this.att_StudentId;
        }
        public void setAttendanceState(int attendanceState)
        {
            this.attendanceState = attendanceState;
        }
        public int getAttendanceState()
        {
            return attendanceState;
        }
        public void setAtt_groupId(int att_groupId)
        {
            this.att_groupId = att_groupId;
        }
        public int getAtt_groupId()
        {
            return this.att_groupId;
        }








    }
}
