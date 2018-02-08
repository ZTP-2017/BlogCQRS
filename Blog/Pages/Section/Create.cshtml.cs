using System.Threading.Tasks;
using Akka.Actor;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Section
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public SectionModel Section { get; set; }
        
        private readonly IActorRef _commandRootActor;
        
        public CreateModel(IActorRefFactory actorRefFactory)
        {
            _commandRootActor = actorRefFactory.ActorOf<CommandRootActor>();
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

            return RedirectToPage(!result.Success ? "/Error" : "/Index");
        }
    }
}