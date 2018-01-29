using System.Threading.Tasks;
using Akka.Actor;
using Blog.Common;
using Blog.ReadSide;
using Blog.ReadSide.Model;
using Blog.ReadSide.Query;
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
        
        public DetailsModel(IActorRefFactory actorRefFactory)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Index");
            }

            var article = await _queryRootActor
                .Ask<ArticleDetailsRecord>(new GetArticleDetails(id.Value));

            ArticleModel = Helpers.MapToArticleModel(article);

            return Page();
        }
    }
}