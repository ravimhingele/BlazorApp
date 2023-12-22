using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class Actor
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        //public string MovieIDs { get; set; }
        [NotMapped]
        public HashSet<Movie> Movies = new HashSet<Movie>();

        public Actor() { }

        public Actor(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }
    }
}
