namespace BlazorApp.Data
{
    public class MovieTitle
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public int Ordering { get; set; }
        public Movie Movie { get; set; }
        public MovieTitle() { }
        public MovieTitle(string title, string movieId, Movie movie, int ordering)
        {
            //Id = id;
            Title = title;
            MovieId = movieId;
            Movie = movie;
            Ordering = ordering;
        }
    }
}
