using System;
using Core.CQRS.Command;

namespace Blog.WriteSide.Command
{
    public class AddArticleCommand : ICommand
    {
        public int SectionId { get; set; }
        public string Title { get; }
        public DateTime Date { get; }
        public string Text { get; }
        public string ImageUrl { get; }
        
        public AddArticleCommand(int sectionId, string title, DateTime date, string text, string imageUrl)
        {
            SectionId = sectionId;
            Title = title;
            Date = date;
            Text = text;
            ImageUrl = imageUrl;
        }
    }
}