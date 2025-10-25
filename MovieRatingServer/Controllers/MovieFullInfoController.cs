using Microsoft.AspNetCore.Mvc;
using MovieRatingServer.Services;
using System.Net;

namespace MovieRatingServer.Controllers;

[ApiController]

public class MovieFullInfoController : ControllerBase
{
    private readonly ILogger<MovieFullInfoController> _logger;
    private readonly IMovieFullListService _movieFullListService;

    public MovieFullInfoController(ILogger<MovieFullInfoController> logger, IMovieFullListService movieFullService)
    {
        _logger = logger;
        _movieFullListService = movieFullService;
    }

    [HttpGet("MovieFullInfo")]
    public IEnumerable<MovieFullInfo> Get()
    {
        return [_movieFullListService.GetRandomFullMovie(), _movieFullListService.GetRandomFullMovie(), _movieFullListService.GetRandomFullMovie()];
    }
}