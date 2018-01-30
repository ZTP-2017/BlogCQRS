using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Blog.ViewModels
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public int SectionId { get; set; }
    }
    
    public class ArticleModelValidator : AbstractValidator<ArticleModel> 
    {
        public ArticleModelValidator()
        {
            RuleFor(x => x.Title).NotNull()
                .WithMessage("Title is required.");
            
            RuleFor(x => x.Text).NotNull()
                .WithMessage("Text is required.");
        }
    }
}