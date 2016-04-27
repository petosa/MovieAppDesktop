using System;
using System.Collections.Generic;
using System.Linq;

namespace Kumquat.NET.model {
    class Movie : IComparable<Movie> {

        private readonly String title;
        private List<Rating> ratings;
        private float averageRating;
        private Dictionary<String, List<float>> majorRatings;
        private String imgURL;

        public Movie(String title) {
            this.title = title;
            ratings = new List<Rating>();
            majorRatings = new Dictionary<String, List<float>>();
        }

        public void addRating(Rating r) {
            ratings.Add(r);

            float aggregateRating = (ratings.Count() - 1) * averageRating;

            averageRating = (aggregateRating + ratings[ratings.Count() - 1].getRating()) / ratings.Count();

            String major = r.getPoster().getProfile().getMajor();

            if (majorRatings.ContainsKey(major)) {
                majorRatings[major].Add(r.getRating());
            } else {
                majorRatings.Add(major, new List<float>());
                majorRatings[major].Add(r.getRating());
            }
        }

        public Dictionary<String, List<float>> getMajorRatings() { return majorRatings; }
        public void setMajorRatings(Dictionary<String, List<float>> majorRatings) { this.majorRatings = majorRatings; }

        public float getAverageMajorRating(String major) {
            if (majorRatings.ContainsKey(major)) {
                List<float> listRatings = majorRatings[major];
                float aggregateRating = 0;

                foreach (float f in listRatings) {
                    aggregateRating += f;
                }

                return aggregateRating / listRatings.Count();
            } else {
                return 0;
            }
        }

        public String getURL() { return imgURL; }
        public String getTitle() { return title; }
        public float getAverageRating() { return averageRating; }
        public List<Rating> getRatings() { return ratings; }

        public void setURL(String imgURL) { this.imgURL = imgURL; }
        public void setAverageRating(float averageRating) { this.averageRating = averageRating; }
        public void setRatings(List<Rating> ratings) { this.ratings = ratings; }

        public int CompareTo(Movie other) {
            if (this.getAverageRating() > other.getAverageRating()) {
                return -1;
            } else if (this.getAverageRating() == other.getAverageRating()) {
                return 0;
            } else {
                return 1;
            }
        }

        public String toString() {
            return this.title + " : " + this.averageRating;
        }
    }
}
