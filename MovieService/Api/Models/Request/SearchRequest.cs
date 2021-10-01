using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MovieService.Api.Models.Request
{
    [Serializable]
    public class SearchRequest
    {
        [FromQuery(Name = "title")]
        public string Title { get; set; }
    }

    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        public SearchRequestValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}