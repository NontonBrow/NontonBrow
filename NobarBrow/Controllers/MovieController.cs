using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using NobarBrow.Models;
using Newtonsoft.Json.Linq;

namespace NobarBrow.Controllers
{
    public class MovieController : Controller
    {
        // GET
        public ActionResult Index()
        {
            var nowPlayingMovies = new List<MovieModel>();
            var trendingMovies = new List<MovieModel>();
            
            string apiKey = "97881e91dfb4b973f6da04e1777abc72";
            string region = "ID";
            
            var responseNowPlayingMovies = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}&region={region}").Result;
            var responseTrendingMovies = GetJsonObjectFromApi($"https://api.themoviedb.org/3/trending/all/week?api_key={apiKey}&region={region}").Result;
            

            if (responseNowPlayingMovies["results"] != null)
            {
                foreach (var result in responseNowPlayingMovies["results"])
                {
                    nowPlayingMovies.Add(new MovieModel()
                    {
                        Id = result["id"].ToString(),
                        PosterPath = "https://image.tmdb.org/t/p/original" + (result["poster_path"] ?? ""),
                        Title = (result["original_title"] ?? "").ToString(),
                        Rating = (result["vote_average"] ?? "").ToString()
                    });
                }
            }
            
            if (responseTrendingMovies["results"] != null)
            {
                foreach (var result in responseTrendingMovies["results"])
                {
                    trendingMovies.Add(new MovieModel()
                    {
                        Id = result["id"].ToString(),
                        PosterPath = "https://image.tmdb.org/t/p/original" + (result["poster_path"] ?? ""),
                        Title = (result["original_title"] ?? "").ToString(),
                        Rating = (result["vote_average"] ?? "").ToString()
                    });
                }
            }
            

            return View(new List<object> { nowPlayingMovies, trendingMovies });
        }
        
        public ActionResult InTheaters()
        {
            var nowPlayingMovies = new List<MovieModel>();
            
            string apiKey = "97881e91dfb4b973f6da04e1777abc72";
            string region = "ID";
            
            var responseNowPlayingMovies = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}&region={region}").Result;
            
            if (responseNowPlayingMovies["results"] != null)
            {
                foreach (var result in responseNowPlayingMovies["results"])
                {
                    nowPlayingMovies.Add(new MovieModel()
                    {
                        Id = result["id"].ToString(),
                        PosterPath = "https://image.tmdb.org/t/p/original" + (result["poster_path"] ?? ""),
                        Title = (result["original_title"] ?? "").ToString(),
                        Rating = (result["vote_average"] ?? "").ToString()
                    });
                }
            }

            return View(nowPlayingMovies);
        }
        
        public ActionResult Popular()
        {
            var popularMovies = new List<MovieModel>();
            
            string apiKey = "97881e91dfb4b973f6da04e1777abc72";
            string region = "ID";
            
            var responseNowPlayingMovies = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}&region={region}").Result;
            
            if (responseNowPlayingMovies["results"] != null)
            {
                foreach (var result in responseNowPlayingMovies["results"])
                {
                    popularMovies.Add(new MovieModel()
                    {
                        Id = result["id"].ToString(),
                        PosterPath = "https://image.tmdb.org/t/p/original" + (result["poster_path"] ?? ""),
                        Title = (result["original_title"] ?? "").ToString(),
                        Rating = (result["vote_average"] ?? "").ToString()
                    });
                }
            }

            return View(popularMovies);
        }
        
        public ActionResult Search()
        {
            return View();
        }
        
        public ActionResult Detail(string id)
        {
            var detailMovie = new DetailMovieModel();
            var castMovie = new List<CastModel>();
            var crewMovie = new List<CrewModel>();
            var similarMovie = new List<MovieModel>();
            
            string apiKey = "97881e91dfb4b973f6da04e1777abc72";
            string idMovie = id;
            
            Console.WriteLine(idMovie);
            var responseDetailMovie = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/{idMovie}?api_key={apiKey}").Result;
            var responseCreditMovie = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/{idMovie}/credits?api_key={apiKey}").Result;
            var responseSimilarMovie = GetJsonObjectFromApi($"https://api.themoviedb.org/3/movie/{idMovie}/similar?api_key={apiKey}").Result;
            
            if (responseDetailMovie != null)
            {
                detailMovie.Id = (int) responseDetailMovie["id"];
                detailMovie.PosterPath = "https://image.tmdb.org/t/p/original" + (responseDetailMovie["poster_path"] ?? "");
                detailMovie.BackdropPath = "https://image.tmdb.org/t/p/original" + (responseDetailMovie["backdrop_path"] ?? "");
                detailMovie.Title = (responseDetailMovie["original_title"] ?? "").ToString();
                detailMovie.Rating = (responseDetailMovie["vote_average"] ?? "").ToString();
                detailMovie.Duration = (responseDetailMovie["runtime"] ?? "").ToString();
                detailMovie.Overview = responseDetailMovie["overview"].ToString();
                detailMovie.ReleaseDate = (responseDetailMovie["release_date"] ?? "").ToString();
                var genres = new List<string>();
                foreach (var genre in responseDetailMovie["genres"]) {
                    genres.Add(genre["name"].ToString());
                };
                detailMovie.Genres = genres.ToArray();
            }

            if (responseCreditMovie != null)
            {
                foreach (var cast in responseCreditMovie["cast"])
                {
                    castMovie.Add(new CastModel()
                    {
                        Id = (int) cast["id"],
                        Name = cast["name"].ToString(),
                        Gender = (int) cast["gender"],
                        ProfilePath = "https://image.tmdb.org/t/p/original" + cast["profile_path"].ToString(),
                        Character = cast["character"].ToString(),
                    });
                }

                foreach (var crew in responseCreditMovie["crew"])
                {
                    if (crew["job"].ToString() == "Producer")
                    {
                        crewMovie.Add(new CrewModel()
                        {
                            Id = (int) crew["id"],
                            Name = crew["name"].ToString(),
                            Gender = (int) crew["gender"],
                            ProfilePath = "https://image.tmdb.org/t/p/original" + crew["profile_path"].ToString(),
                            Job = crew["job"].ToString(),
                        });
                    }
                }
            }

            if (responseSimilarMovie != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    similarMovie.Add(new MovieModel()
                    {
                        Id = responseSimilarMovie["results"][i]["id"].ToString(),
                        PosterPath = "https://image.tmdb.org/t/p/original" + (responseSimilarMovie["results"][i]["poster_path"] ?? ""),
                        Title = (responseSimilarMovie["results"][i]["original_title"] ?? "").ToString(),
                        Rating = (responseSimilarMovie["results"][i]["vote_average"] ?? "").ToString()
                    });
                }
            }
            
            return View(new List<object> { detailMovie, castMovie, crewMovie, similarMovie });
        }
        
        private async Task<JObject> GetJsonObjectFromApi(string apiUrl)
        {
            Console.WriteLine(apiUrl);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(responseContent);

                    return jsonObject;
                }
                else
                {
                    // An error occurred, return an empty JSON object
                    Console.WriteLine(response.StatusCode);
                    return new JObject();
                }
            }
        }
    }
}