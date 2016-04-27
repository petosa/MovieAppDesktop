using System;

namespace Kumquat.NET.model {
    class Profile {
        private String major = "CS";
        private String desc = "Add description.";

        public Profile(String major, String desc) {
            this.major = major;
            this.desc = desc;
        }

        public String getMajor() { return major; }
        public String getDesc() { return desc; }

        public void setMajor(String major) { this.major = major; }
        public void setDesc(String desc) { this.desc = desc; }
    }
}
