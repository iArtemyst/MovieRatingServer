using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace MovieRatingServer.Services;

public class MovieFullListService : IMovieFullListService
{
    private readonly List<MovieFullInfo> _fullmovies;
    private readonly Random _rng;

    public MovieFullListService(IWebHostEnvironment env)
    {
        _rng = new Random();

        var path = Path.Combine(env.ContentRootPath, "movie-database.json");

        if (!File.Exists(path))
        {
            _fullmovies = new List<MovieFullInfo>();
            return;
        }

        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var root = JsonSerializer.Deserialize<RawRoot>(json, options);
        _fullmovies = root?.MovieDatabase?.Select(FullMap).ToList() ?? new List<MovieFullInfo>();
    }

    public MovieFullInfo GetRandomFullMovie()
    {
        if (_fullmovies.Count == 0)
            return new MovieFullInfo { Title = "No movies available" };

        return _fullmovies[_rng.Next(_fullmovies.Count)];
    }

    private static MovieFullInfo FullMap(RawMovie m)
    => new MovieFullInfo
    {
        Title = m.Title ?? string.Empty,
        Year = m.Year ?? string.Empty,
        Rated = m.Rated ?? string.Empty,
        Released = m.Released ?? string.Empty,
        Runtime = m.Runtime ?? string.Empty,
        Genre = m.Genre ?? string.Empty,
        Director = m.Director ?? string.Empty,
        Writer = m.Writer ?? string.Empty,
        Actors = m.Actors ?? string.Empty,
        Plot = m.Plot ?? string.Empty,
        Language = m.Language ?? string.Empty,
        Country = m.Country ?? string.Empty,
        Awards = m.Awards ?? string.Empty,
        Poster = m.Poster ?? string.Empty,
        Ratings = (m.Ratings ?? Enumerable.Empty<Rating>())
                    .Select(r => new MovieFullInfoRating
                    {
                        Source = r.Source ?? string.Empty,
                        Value = r.Value ?? string.Empty
                    })
                    .ToList(),
        Metascore = m.Metascore ?? string.Empty,
        ImdbRating = m.imdbRating ?? string.Empty,
        ImdbVotes = m.imdbVotes ?? string.Empty,
        ImdbID = m.imdbID ?? string.Empty,
        Type = m.Type ?? string.Empty,
        DVD = m.DVD ?? string.Empty,
        BoxOffice = m.BoxOffice ?? string.Empty,
        Production = m.Production ?? string.Empty,
        Website = m.Website ?? string.Empty,
        Response = m.Response ?? string.Empty
    };


    private class RawRoot
    {
        [JsonPropertyName("MovieDatabase")]
        public List<RawMovie> MovieDatabase { get; set; } = new();
    }

    private class RawMovie
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
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