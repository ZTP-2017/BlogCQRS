using Blog.ReadSide.Model;
using FluentValidation;

namespace Blog.ViewModels
{
    public class SectionModel
    {
        public string Name { get; set; }
        public ArticleListItemRecord[] Articles { get; set; }
    }
    
    public class SectionModelValidator : AbstractValidator<SectionModel> 
    {
        public SectionModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name is required.");
        }
    }
}