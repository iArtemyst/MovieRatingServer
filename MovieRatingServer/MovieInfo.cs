using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieRatingServer;

public class MovieDatabaseRoot
{
    public List<MovieFullInfo> MovieDatabase { get; set; }
}

public class MovieInfo
{
    [JsonPropertyName("movieTitle")]
    public string movieTitle { get; set; } = string.Empty;

    [JsonPropertyName("movieDirector")]
    public string movieDirector { get; set; } = string.Empty;

    [JsonPropertyName("movieReleaseYear")]
    public string movieReleaseYear { get; set; } = string.Empty;

    [JsonPropertyName("movieTopBilled")]
    public string movieTopBilled { get; set; } = string.Empty;

    [JsonPropertyName("movieSummary")]
    public string movieSummary { get; set; } = string.Empty;

    [JsonPropertyName("moviePosterLink")]
    public string moviePosterLink { get; set; } = string.Empty;

    [JsonPropertyName("movieRatingIMDB")]
    public string movieRatingIMDB { get; set; } = string.Empty;

    [JsonPropertyName("movieRatingMetascore")]
    public string movieRatingMetascore { get; set; } = string.Empty;

    [JsonPropertyName("movieRatingRottenTomatoes")]
    public string movieRatingRottenTomatoes { get; set; } = string.Empty;

    [JsonPropertyName("movieRatingOther")]
    public string movieRatingOther { get; set; } = string.Empty;
}



public class MovieFullInfo
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Writer { get; set; }
    public string Actors { get; set; }
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public string Awards { get; set; }
    public string Poster { get; set; }
    public List<MovieFullInfoRating> Ratings { get; set; }
    public string Metascore { get; set; }

    [JsonPropertyName("imdbRating")]
    public string ImdbRating { get; set; }

    [JsonPropertyName("imdbVotes")]
    public string ImdbVotes { get; set; }

    [JsonPropertyName("imdbID")]
    public string ImdbID { get; set; }

    public string Type { get; set; }
    public string DVD { get; set; }
    public string BoxOffice { get; set; }
    public string Production { get; set; }
    public string Website { get; set; }
    public string Response { get; set; }
}

public class MovieFullInfoRating
{
    public string Source { get; set; }
    public string Value { get; set; }
}