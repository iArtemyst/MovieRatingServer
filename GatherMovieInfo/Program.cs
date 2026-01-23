using MovieRating.Shared;
using Services;

namespace GatherMovieInfo;

internal class Program
{
    private const string movieFightClubID = "550";
    private const int startingPageNumber = 1;
    private const int totalPagesToFetch = 42;
    private const int minAverageVote = 4;
    private const int maxAverageVote = 7;
    private const int minVoteCount = 3000;

    public static async Task Main(string[] args)
    {
        TMDBService tmdbService = new TMDBService();
        //await tmdbService.RequestMovieWithParameters(startingPageNumber, totalPagesToFetch, minAverageVote, maxAverageVote, minVoteCount);
        //await tmdbService.CombineJsonFiles();
        //await tmdbService.PullMovieIDsFromList();
        await tmdbService.GetIDofIMDBfromTMDB();
    }
}
