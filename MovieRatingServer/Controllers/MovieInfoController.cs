using Microsoft.AspNetCore.Mvc;
using MovieRating.Shared;
using MovieRatingServer.Services;

namespace MovieRatingServer.Controllers;

[ApiController]
public class MovieInfoController : ControllerBase
{
    private readonly ILogger<MovieInfoController> _logger;
    private readonly IMovieListService _movieListService;

    public MovieInfoController(ILogger<MovieInfoController> logger, IMovieListService movieService)
    {
        _logger = logger;
        _movieListService = movieService;
    }

    [HttpGet("MovieInfo")]
    public IEnumerable<MovieInfo> Get()
    {
        var result = new List<MovieInfo>(3);
        var seenTitles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        const int maxAttempts = 50;
        int attempts = 0;

        while (result.Count < 3 && attempts < maxAttempts)
        {
            var movie = _movieListService.GetRandomMovie();
            attempts++;

            if (movie is null)
                continue;

            // Use Title as the uniqueness key; change to another property if needed
            if (seenTitles.Add(movie.Title))
            {
                result.Add(movie);
            }
        }

        return result;
    }
}