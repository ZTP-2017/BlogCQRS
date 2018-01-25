using System;
using Core.CQRS.Command;

namespace Blog.WriteSide.Command.Article
{
    public class AddArticleCommand : ICommand
    {
        public string Title { get; }
        public DateTime Date { get; }
        public string Text { get; }
        public string Image { get; }

        public AddArticleCommand()
        {
            
        }
        
        public AddArticleCommand(string title, DateTime date, string text, string image)
        {
            Title = title;
            Date = date;
            Text = text;
            Image = image;
        }
    }
}