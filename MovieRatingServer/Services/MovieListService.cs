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
            _movies = root?.MovieDatabase?.Select(m => Map(m, _rng)).ToList() ?? new List<MovieInfo>();
        }
        else
        {
            _movies = new List<MovieInfo>();
        }
    }

    public int GetRadomValueBetween0AndX(int x)
    {
        return _rng.Next(0, x);
    }

    public MovieInfo GetRandomMovie()
    {
        return _movies[_rng.Next(_movies.Count)];
    }

    private static MovieInfo Map(RawMovie m, Random rng)
    {
        int ratingsCount = m.Ratings?.Count ?? 0;
        int random = ratingsCount > 0 ? rng.Next(0, ratingsCount) : 0;
        var (source, value) = PickRandomRating(m, random, rng);

        return new MovieInfo
        {
            Title = m.Title,
            Year = m.Year ?? string.Empty,
            Director = m.Director ?? string.Empty,
            Actors = m.Actors ?? string.Empty,
            Plot = m.Plot ?? string.Empty,
            Poster = m.Poster ?? string.Empty,
            RatingSource = source,
            RatingValue = value,
            RandomRatingInt = random,
        };
    }

    private static (string Source, string Value) PickRandomRating(RawMovie m, int x, Random rng)
    {
        if (m.Ratings is { Count: > 0 })
        {
            var r = m.Ratings[x];
            return (r.Source ?? string.Empty, r.Value ?? string.Empty);
        }

        if (!string.IsNullOrEmpty(m.imdbRating))
            return ("Internet Movie Database", m.imdbRating);

        return (string.Empty, string.Empty);
    }

    private class RawRoot
    {
        [JsonPropertyName("MovieDatabase")]
        public List<RawMovie> MovieDatabase { get; set; } = new();
    }
}