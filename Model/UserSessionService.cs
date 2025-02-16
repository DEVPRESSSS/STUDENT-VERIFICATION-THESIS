using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class UserSessionService
    {
        private static UserSessionService _instance;

        public static UserSessionService Instance => _instance ??= new UserSessionService();

        public string LoggedInStaffID { get; private set; }
        public string Username { get; private set; }

        private UserSessionService() { }

        public void SetLoggedInUser(string staffID, string username)
        {
            LoggedInStaffID = staffID;
            Username = username;
        }
    }
}
