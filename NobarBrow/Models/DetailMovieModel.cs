namespace NobarBrow.Models
{
    public class DetailMovieModel
    {
        public int Id { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Rating { get; set; }
        public string[] Genres { get; set; }
        public string Duration { get; set; }
        public string ReleaseDate { get; set; }
    }
}