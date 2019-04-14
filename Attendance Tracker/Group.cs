using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Tracker
{
    class Group
    {
        private String groupName;
        private int groupId;
        public void setGroupName(String groupName)
        {
            this.groupName = groupName;       }
        public String getGroupName()
        {
            return this.groupName;
        }
        public void setGroupId(int groupId)
        {
            this.groupId = groupId;
        }
        public int getGroupId()
        {
            return this.groupId;
        }
        public static Group selectedGroup = new Group();
       


    }
}
