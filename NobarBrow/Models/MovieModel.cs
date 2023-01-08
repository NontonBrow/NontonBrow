using Newtonsoft.Json.Linq;

namespace NobarBrow.Models
{
    public class MovieModel
    {
        public string Id { get; set; }
        public string PosterPath { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
    }
}