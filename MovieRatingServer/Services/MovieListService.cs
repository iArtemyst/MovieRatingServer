using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace MovieRatingServer.Services;

public class MovieListService : IMovieListService
{
    private readonly List<MovieInfo> _movies;
    private readonly Random _rng;

    public MovieListService(IWebHostEnvironment env)
    {
        _rng = new Random();

        var path = Path.Combine(env.ContentRootPath, "movie-database.json");

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var root = JsonSerializer.Deserialize<RawRoot>(json, options);
            _movies = root?.MovieDatabase?.Select(m => Map(m)).ToList() ?? new List<MovieInfo>();
        }
        else
        {
            _movies = new List<MovieInfo>();
        }
    }

    public MovieInfo GetRandomMovie()
    {
        if (_movies.Count == 0)
            return new MovieInfo { movieTitle = "No movies available" };

        return _movies[_rng.Next(_movies.Count)];
    }

    private static MovieInfo Map(RawMovie m)
        => new MovieInfo
        {
            movieTitle = m.Title ?? string.Empty,
            movieDirector = m.Director ?? string.Empty,
            movieReleaseYear = m.Year ?? string.Empty,
            movieTopBilled = m.Actors ?? string.Empty,
            movieSummary = m.Plot ?? string.Empty,
            moviePosterLink = m.Poster ?? string.Empty,
            movieRatingIMDB = m.imdbRating ?? string.Empty,
            movieRatingMetascore = m.Metascore ?? string.Empty,
            movieRatingRottenTomatoes = m.Ratings?.FirstOrDefault(r => r.Source == "Rotten Tomatoes")?.Value ?? string.Empty,
            movieRatingOther = string.Join(", ",
                (m.Ratings ?? Enumerable.Empty<Rating>())
                    .Where(r => r.Source != "Rotten Tomatoes" && r.Source != "Internet Movie Database" && r.Source != "Metacritic")
                    .Select(r => $"{r.Source}: {r.Value}"))
        };

    private class RawRoot
    {
        [JsonPropertyName("MovieDatabase")]
        public List<RawMovie> MovieDatabase { get; set; } = new();
    }

    private class RawMovie
    {
        public required string Title { get; set; }
        public string Year { get; set; } = string.Empty;
        public string? Rated { get; set; }
        public string? Released { get; set; }
        public string? Runtime { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Writer { get; set; }
        public string? Actors { get; set; }
        public string? Plot { get; set; }
        public string? Language { get; set; }
        public string? Country { get; set; }
        public string? Awards { get; set; }
        public string? Poster { get; set; }
        public List<Rating>? Ratings { get; set; }
        public string? Metascore { get; set; }
        public string? imdbRating { get; set; }
        public string? imdbVotes { get; set; }
        public string? imdbID { get; set; }
        public string? Type { get; set; }
        public string? DVD { get; set; }
        public string? BoxOffice { get; set; }
        public string? Production { get; set; }
        public string? Website { get; set; }
        public string? Response { get; set; }
    }

    private class Rating
    {
        public string? Source { get; set; }
        public string? Value { get; set; }
    }
}