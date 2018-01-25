using System.Threading.Tasks;
using Akka.Actor;
using Blog.Common;
using Blog.Context.Model;
using Blog.ReadSide;
using Blog.ReadSide.Query.Article;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public ArticleModel ArticleModel { get; set; }
        
        private readonly IActorRef _queryRootActor;
        
        public DetailsModel(ActorSystem actorSystem)
        {
            _queryRootActor = actorSystem.ActorOf<QueryRootActor>();
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Index");
            }

            var article = await _queryRootActor
                .Ask<ArticleRecord>(new GetArticleQuery(id.Value));

            ArticleModel = Helpers.MapArticleRecordToArticleModel(article);

            return Page();
        }
    }
}