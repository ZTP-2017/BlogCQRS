using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.Common;
using Blog.Context.Model;
using Blog.ReadSide;
using Blog.ReadSide.Query.Article;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command.Article;
using Core.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        public ArticleModel[] Articles { get; set; }
        
        private readonly IActorRef _commandRootActor;
        private readonly IActorRef _queryRootActor;

        public IndexModel(ActorSystem actorSystem)
        {
            _commandRootActor = actorSystem.ActorOf<CommandRootActor>();
            _queryRootActor = actorSystem.ActorOf<QueryRootActor>();
        }
        
        public async Task OnGetAsync()
        {
            Articles = (await _queryRootActor
                .Ask<IEnumerable<ArticleRecord>>(new GetArticlesListQuery()))
                .Select(Helpers.MapArticleRecordToArticleModel)
                .ToArray();
        }
        
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _commandRootActor.Ask<CommandResult>(new RemoveArticleCommand(id));

            return RedirectToPage("/Index");
        }
    }
}
