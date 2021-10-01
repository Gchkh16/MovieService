using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MovieService.Api.Models.Request
{
    [Serializable]
    public class MovieRequest
    {
        [FromQuery(Name = "userId")] 
        public int UserId { get; set; }
        
        [FromQuery(Name = "movieId")]
        public string MovieId { get; set; }
        
    }
    
    public class MovieRequestValidator : AbstractValidator<MovieRequest>
    {
        public MovieRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.MovieId).NotNull().NotEmpty().MaximumLength(20).Must(x => x.StartsWith("tt"));
        }
    }
}