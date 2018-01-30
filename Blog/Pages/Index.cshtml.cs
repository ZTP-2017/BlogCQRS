using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.Common;
using Blog.ReadSide;
using Blog.ReadSide.Model;
using Blog.ReadSide.Query;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        public List<SectionModel> Sections { get; set; }
        
        private readonly IActorRef _queryRootActor;

        public IndexModel(IActorRefFactory actorRefFactory)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            Sections = new List<SectionModel>();
            
            var sections = (await _queryRootActor
                .Ask<IEnumerable<SectionDetailsRecord>>(new GetSectionList()))
                .ToArray();
            
            foreach (var section in sections)
            {
                var sectionArticles = await _queryRootActor
                    .Ask<IEnumerable<ArticleListItemRecord>>(new GetSectionArticleListItems(section.Id));
                
                Sections.Add(Helpers.MapToSectionModel(section, sectionArticles));
            }

            return Page();
        }
    }
}
