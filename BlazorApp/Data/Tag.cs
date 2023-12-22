using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class Tag
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public HashSet<Movie> Movies = new HashSet<Movie>();

        public Tag() { }

        public Tag(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddMovie(Movie movieName)
        {
            Movies.Add(movieName);
        }
    }
}
