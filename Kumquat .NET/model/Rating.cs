using System;
using System.Collections.Generic;

namespace Kumquat.NET.model {
    class Rating {
        private readonly float rating;
        private readonly String comment;
        private readonly User poster;

        public Rating(float rating, String comment, User poster) {
            this.rating = rating;
            this.comment = comment;
            this.poster = poster;
        }

        public float getRating() { return rating; }
        public String getComment() { return comment; }
        public User getPoster() { return poster; }

        public Dictionary<String, String> toMap() {
            Dictionary<String, String> toMap = new Dictionary<String, String>();

            toMap.Add("rating", rating.ToString());
            toMap.Add("comment", comment);
            toMap.Add("user", poster.getUsername());

            return toMap;
        }
    }
}
