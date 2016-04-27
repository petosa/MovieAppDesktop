using Kumquat.NET.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kumquat.NET
{
    static class UserManager
    {
        public static Dictionary<String, String> ub = new Dictionary<String, String>();
        
        public static void addAccount(String n, String e, String u, String p)
        {
            User user = new User(n, e, u, p);
            DBHelper.addUser(user);
        }

        public static Boolean userExists(String username)
        {

            if (ub != null && ub.ContainsKey(username))
                return true;
            return false;
        }

        public static Boolean authenticate(String username, String password)
        {
            if (ub!= null && ub.ContainsKey(username) && ub[username].Equals(password))
                return true;
            return false;
        }
    }
}
