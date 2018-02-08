using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Akka.Actor;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public DetailsModel(IActorRefFactory actorRefFactory, IMapper mapper)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
            _mapper = mapper;
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Index");
            }

            var article = await _queryRootActor
                .Ask<ArticleDetailsRecord>(new GetArticleDetailsQuery(id.Value));

            ArticleModel = _mapper.Map<ArticleModel>(article);

            return Page();
        }
    }
}