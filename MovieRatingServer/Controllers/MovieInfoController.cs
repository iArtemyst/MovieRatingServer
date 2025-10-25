using Microsoft.AspNetCore.Mvc;
using MovieRatingServer.Services;
using System.Net;

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
        return [_movieListService.GetRandomMovie(), _movieListService.GetRandomMovie(), _movieListService.GetRandomMovie()];
    }
}