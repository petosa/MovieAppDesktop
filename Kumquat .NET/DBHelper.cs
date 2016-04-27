using Kumquat.NET.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kumquat.NET {
    static class DBHelper {

        private static User currentUser = null;
        private static Dictionary<String, User> allUsers = new Dictionary<String, User>();
        private static Dictionary<String, Movie> allMovies = new Dictionary<String, Movie>();

        public static User getCurrentUser() { return currentUser; }
        public static Dictionary<String, User> getUsersMap() { return allUsers; }
        public static Dictionary<String, Movie> getMoviesMap() { return allMovies; }

        public static void setCurrentUser(User u) { currentUser = u; }

        public static String getDigest(String password) {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] key = System.Text.Encoding.Default.GetBytes(password);
            byte[] hash = sha256.ComputeHash(key);

            StringBuilder sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash) {
                sb.Append(Convert.ToInt32(b).ToString("X2"));
            }

            return sb.ToString();
        }

        public static void startListeners() {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("javaw.exe", "-jar DBHelper.jar");
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void runCommand(String exe, String param) {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(exe, param);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void runDBCLI(String param) {
            runCommand("javaw.exe", "-jar DBCLI.jar " + param);
        }

        public static List<User> getAllUsers() { return allUsers.Values.ToList(); }

        public static Boolean isUser(String username) {
            return allMovies.ContainsKey(username);
        }

        public static Boolean addUser(User u) {
            u.setStatus("Active");

            StringBuilder sb = new StringBuilder();
            sb.Append("\"addUser\" ");
            sb.Append("\"");
            if (u.getProfile() != null) {
                sb.Append(u.getProfile().getDesc());
            }
            sb.Append("\" ");
            sb.Append("\"");
            sb.Append(u.getEmail());
            sb.Append("\" ");
            sb.Append("\"");
            if (u.getProfile() != null) {
                sb.Append(u.getProfile().getMajor());
            }
            sb.Append("\" ");
            sb.Append("\"");
            sb.Append(u.getName());
            sb.Append("\" ");
            sb.Append("\"");
            sb.Append(u.getPasswordHash());
            sb.Append("\" ");
            sb.Append("\"");
            sb.Append(u.getStatus());
            sb.Append("\" ");
            sb.Append("\"");
            sb.Append(u.getUsername());
            sb.Append("\"");

            runDBCLI(sb.ToString());

            allUsers.Add(u.getUsername(), u);
            return allUsers.ContainsKey(u.getUsername());
        }

        public static User getUser(String username) {
            return allUsers[username];
        }

        public static void lockUser(String username) {
            setStatus(username, "Locked");
        }

        public static void banUser(String username) {
            setStatus(username, "Banned");
        }

        public static void activateUser(String username) {
            setStatus(username, "Active");
        }

        public static void setDescription(String username, String description) {
            allUsers[username].getProfile().setDesc(description);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"description\" ");
            sb.Append("\"" + description + "\"");
            runDBCLI(sb.ToString());
        }

        public static void setEmail(String username, String email) {
            allUsers[username].setEmail(email);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"email\" ");
            sb.Append("\"" + email + "\"");
            runDBCLI(sb.ToString());
        }

        public static void setMajor(String username, String major) {
            allUsers[username].getProfile().setMajor(major);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"major\" ");
            sb.Append("\"" + major + "\"");
            runDBCLI(sb.ToString());
        }

        public static void setName(String username, String name) {
            allUsers[username].setName(name);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"name\" ");
            sb.Append("\"" + name + "\"");
            runDBCLI(sb.ToString());
        }

        public static void setPasswordHash(String username, String passwordHash) {
            allUsers[username].setPasswordHash(passwordHash);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"passwordHash\" ");
            sb.Append("\"" + passwordHash + "\"");
            runDBCLI(sb.ToString());
        }

        public static void setStatus(String username, String status) {
            allUsers[username].setStatus(status);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"uset\" ");
            sb.Append("\"users/" + username + "\" ");
            sb.Append("\"status\" ");
            sb.Append("\"" + status + "\"");
            runDBCLI(sb.ToString());
        }

        public static String getDescription(String username) {
            return allUsers[username].getProfile().getDesc();
        }

        public static String getEmail(String username) {
            return allUsers[username].getEmail();
        }

        public static String getMajor(String username) {
            return allUsers[username].getProfile().getMajor();
        }

        public static String getName(String username) {
            return allUsers[username].getName();
        }
        
        public static String getPasswordHash(String username) {
            return allUsers[username].getPasswordHash();
        }

        public static String getStatus(String username) {
            return allUsers[username].getStatus();
        }
    }
}
