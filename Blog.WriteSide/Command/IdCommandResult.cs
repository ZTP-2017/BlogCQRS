using Core.CQRS.Command;

namespace Blog.WriteSide.Command
{
    public class IdCommandResult : CommandResult
    {
        public int Id { get; }

        public IdCommandResult(int id, bool success = true) : base(success)
        {
            Id = id;
        }
    }
}