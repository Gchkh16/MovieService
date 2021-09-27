using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieService.Database;
using MovieService.Database.Models;
using MovieService.Services.Abstractions;
using Quartz;

namespace MovieService.Jobs
{
    public class SuggestionSenderJob : IJob
    {
        private const string SendToMail = "giorgi_chxikva@outlook.com"; // TODO change this;

        private readonly MoviesDb _db;
        private readonly IImDbApiService _imDbApiService;
        private readonly IMailSender _mailSender;

        public SuggestionSenderJob(MoviesDb db, IMailSender mailSender, IImDbApiService imDbApiService)
        {
            _db = db;
            _mailSender = mailSender;
            _imDbApiService = imDbApiService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var savedMoviesToSuggest = _db.SavedMovies.Where(m => !m.Watched)
                .Select(m => m.UserId)
                .Distinct()
                .Select(userId => _db.SavedMovies.Where(m => m.UserId == userId)
                    .Include(m => m.Movie)
                    .Where(NotSentMailLastMonth())
                    .OrderBy(m => m.Movie.ImdbRating)
                    .Last());


            foreach (var movie in savedMoviesToSuggest)
            {
                var plot = await _imDbApiService.GetShortPlot(movie.MovieId);
                var posters = await _imDbApiService.GetPosters(movie.MovieId);
                await _mailSender.SendMailAsync(new MailAddress(SendToMail),
                    "New Suggestion: " + movie.Movie.Title, $"<img src={posters[0].Link}>" + plot.Html);
            }
        }

        private Expression<Func<SavedMovie, bool>> NotSentMailLastMonth()
        {
            var sentAtMax = DateTime.UtcNow - TimeSpan.FromDays(30);

            return m => !_db.SentOffers.Any(o =>
                o.MovieId == m.MovieId && o.UserId == m.UserId && o.SentAtUtc > sentAtMax);
        }
    }
}