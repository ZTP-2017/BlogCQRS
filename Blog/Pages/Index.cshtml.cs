using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide;
using Blog.ReadSide.Model;
using Blog.ReadSide.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        public List<SectionDetailsRecord> Sections { get; set; }
        
        private readonly IActorRef _queryRootActor;

        public IndexModel(IActorRefFactory actorRefFactory)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            Sections = (await _queryRootActor
                .Ask<IEnumerable<SectionDetailsRecord>>(new GetSectionListQuery()))
                .ToList();

            return Page();
        }
    }
}
