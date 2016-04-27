using System;
using System.Collections.Generic;

namespace Kumquat.NET.model {
    class User {
        private String name;
        private String email;
        private String username;
        private String passwordHash;
        private String status;
        private Profile profile;

        public User(String name, String email, String username, String passwordHash) {
            this.name = name;
            this.email = email;
            this.username = username;
            this.passwordHash = passwordHash;
        }

        public String getName() { return name; }
        public String getEmail() { return email; }
        public String getUsername() { return username; }
        public String getPasswordHash() { return passwordHash; }
        public String getStatus() { return status; }
        public Profile getProfile() { return profile; }

        public void setName(String name) { this.name = name; }
        public void setEmail(String email) { this.email = email; }
        public void setUsername(String username) { this.username = username; }
        public void setPasswordHash(String passwordHash) { this.passwordHash = passwordHash; }
        public void setStatus(String status) { this.status = status; }
        public void setProfile(Profile profile) { this.profile = profile; }

        public Dictionary<String, String> toMap() {
            Dictionary<String, String> toMap = new Dictionary<String, String>();

            toMap.Add("name", name);
            toMap.Add("email", email);
            toMap.Add("username", username);
            toMap.Add("passwordHash", passwordHash);
            toMap.Add("status", status);

            if (profile == null) {
                toMap.Add("major", "");
                toMap.Add("description", "");
            } else {
                toMap.Add("major", profile.getMajor());
                toMap.Add("description", profile.getDesc());
            }

            return toMap;
        }
    }
}
