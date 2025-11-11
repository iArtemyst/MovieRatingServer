using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieRatingServer;

public class MovieInfo
{
    [JsonPropertyName("Title")]
    public required string Title { get; set; }

    [JsonPropertyName("Director")]
    public required string Director { get; set; }

    [JsonPropertyName("Year")]
    public required string Year { get; set; }

    [JsonPropertyName("Actors")]
    public required string Actors { get; set; }

    [JsonPropertyName("Plot")]
    public required string Plot { get; set; }

    [JsonPropertyName("Poster")]
    public required string Poster { get; set; }

    [JsonPropertyName("RatingSource")]
    public required string RatingSource { get; set; }

    [JsonPropertyName("RatingValue")]
    public required string RatingValue { get; set; }

    [JsonPropertyName("RandomRatingInt")]
    public required int RandomRatingInt { get; set; }
}


public class RawMovie
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

public class Rating
{
    public string? Source { get; set; }
    public string? Value { get; set; }
}