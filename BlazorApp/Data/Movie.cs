using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class Movie
    {
        [Key]
        public string ID { get; set; } //IMDB
        public HashSet<MovieTitle> Titles = new HashSet<MovieTitle>();
        public string Country { get; set; }
        public string Langage { get; set; }
        public HashSet<Actor> Actors = new HashSet<Actor>();
        public Actor? Director { get; set; }
        public HashSet<Tag> Tags = new HashSet<Tag>();
        public string? Rating = "0.0";
        [NotMapped]
        public LinkedList<Movie> top = new LinkedList<Movie>();
        [NotMapped]
        public LinkedList<int> t_ratings = new LinkedList<int>();
        public string Top10Codes { get; set; } = "";


        public Movie() { }

        public Movie(string id, MovieTitle title, string country, string lang)
        {
            ID = id;
            Titles.Add(title); // затем проверяем, есть ли такой фильм в словаре, если есть добавляем отдельно
            Country = country;
            Langage = lang;
        }
        public Movie(string id, string country, string lang)
        {
            ID = id;
            //Titles.Add(title); // затем проверяем, есть ли такой фильм в словаре, если есть добавляем отдельно
            Country = country;
            Langage = lang;
        }

        public void AddTitle(MovieTitle title)
        {
            Titles.Add(title);
        }
        public void AddActors(Actor actor)
        {
            Actors.Add(actor);
        }
        public void AddTag(Tag tag)
        {
            Tags.Add(tag);
        }

        /*public string GetName()
        {
            return this.Titles.First().Title;
        }*/

        public int GetSimilarity(Movie? movie)
        {
            int answer = Convert.ToInt32(movie.Rating.Replace(".", ""));
            if (Director != null && movie.Director != null && Director == movie.Director)
            {
                answer += 20;
            }
            int amOfActors = Actors.Intersect(movie.Actors).Count();
            answer += amOfActors * 10;

            int amOfTags = Tags.Intersect(movie.Tags).Count();
            answer += amOfTags;

            return answer;
        }


        public void FindSimilarMovies()
        {


            LinkedListNode<Movie> tMovie;
            LinkedListNode<int> tInt;
            HashSet<Movie> closeMovies = new HashSet<Movie>();


            if (Director != null)
            {
                if (Director.Movies != null)
                {
                    foreach (Movie movie in Director.Movies)
                    {
                        closeMovies.Add(movie);
                    }
                }

            }

            if (Actors != null)
            {
                foreach (Actor actor in Actors)
                {
                    if (actor.Movies != null)
                    {
                        foreach (Movie movie in actor.Movies)
                        {
                            closeMovies.Add(movie);
                        }
                    }
                }
            }

            if (Tags != null)
            {
                foreach (Tag tag in Tags)
                {
                    if (tag.Movies != null)
                    {
                        foreach (Movie movie in tag.Movies)
                        {
                            closeMovies.Add(movie);
                        }
                    }
                }
            }
            foreach (Movie movie in closeMovies)
            {
                if (movie != this)
                {
                    int r = GetSimilarity(movie);
                    if (top.Count == 0)
                    {
                        top.AddFirst(movie);
                        t_ratings.AddFirst(r);
                    }
                    else
                    {
                        tMovie = top.First;
                        tInt = t_ratings.First;
                        while (tMovie.Next != null && tInt.Next != null && tInt.Value > r)
                        {
                            tMovie = tMovie.Next;
                            tInt = tInt.Next;
                        }
                        if (tInt.Value < r)
                        {
                            top.AddBefore(tMovie, movie);
                            t_ratings.AddBefore(tInt, r);
                        }
                        else
                        {
                            top.AddLast(movie);
                            t_ratings.AddLast(r);
                        }
                        while (top.Count > 10)
                        {
                            top.RemoveLast();
                            t_ratings.RemoveLast();
                        }
                    }
                }
            }
            foreach (Movie movie in top)
            {
                if (Top10Codes == "")
                {
                    Top10Codes = movie.ID;
                }
                else
                {
                    Top10Codes += ", " + movie.ID;
                }
            }
        }
    }
}
