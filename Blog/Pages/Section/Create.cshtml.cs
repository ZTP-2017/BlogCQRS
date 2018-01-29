using System.Threading.Tasks;
using Akka.Actor;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command;
using Blog.WriteSide.Events;
using Core.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Section
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public SectionModel Section { get; set; }
        
        private readonly IActorRef _commandRootActor;
        private readonly IActorRef _eventRootActor;
        
        public CreateModel(IActorRefFactory actorRefFactory)
        {
            _commandRootActor = actorRefFactory.ActorOf<CommandRootActor>();
            _eventRootActor = actorRefFactory.ActorOf<EventRootActor>();
        }
        
        public void OnGet()
        {
            Section = new SectionModel();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _commandRootActor
                .Ask<IdCommandResult>(new AddSectionCommand(Section.Name));

            if (result.Success)
            {
                await _eventRootActor.Ask<CommandResult>(new SectionAddedEvent(result.Id));
            }

            return RedirectToPage("/Index");
        }
    }
}