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
    }
}
