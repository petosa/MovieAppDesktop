using Kumquat.NET.model;

using System;
using System.Collections.Generic;
using System.Linq;
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
            return password;
        }
    }
}
