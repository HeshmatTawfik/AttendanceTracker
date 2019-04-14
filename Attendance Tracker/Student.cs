using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Tracker
{
    class Student
    {
        private int id;
        private String name;
        private int abscenceCounter;
        private int studentGroup;
        private String absenceLevel;
        public void setAbsenceLevel(String absenceLevel)
        {
            this.absenceLevel = absenceLevel;
        }
        public String getAbsenceLevel()
        {
            return this.absenceLevel;
        }
        public void setId( int id)
        {
            this.id = id;
        
        }
        public int getId()
        {
            return this.id;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return this.name;
        }
        public void setAbscenceCounter(int abscenceCounter)
        {
            this.abscenceCounter = abscenceCounter;
        }
        public int getAbscenceCounter()
        {
            return this.abscenceCounter;
        }
        public void setStudentGroup(int studentGroup)
        {
            this.studentGroup = studentGroup;
        }
        public int getStudentGroup()
        {
            return this.studentGroup;
        }
    }
}
