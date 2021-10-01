using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MovieService.Api.Models.Request
{
    [Serializable]
    public class GetSavedMoviesRequest
    {
        [FromQuery(Name = "userId")]
        public int UserId { get; set; }
    }

    public class GetSavedMoviesRequestValidator : AbstractValidator<GetSavedMoviesRequest>
    {
        public GetSavedMoviesRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}