using Core.CQRS.Command;

namespace Blog.WriteSide.Command
{
    public class AddSectionCommand : ICommand
    {
        public string Name { get; }
        
        public AddSectionCommand(string name)
        {
            Name = name;
        }
    }
}