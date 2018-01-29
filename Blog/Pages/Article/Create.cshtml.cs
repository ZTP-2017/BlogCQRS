using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide;
using Blog.ReadSide.Query;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command;
using Blog.WriteSide.Events;
using Blog.WriteSide.Model.ReadSide;
using Core.CQRS.Command;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Pages.Article
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ArticleModel ArticleModel { get; set; }
        
        [BindProperty]
        public List<SelectListItem> Sections { get; set; }
        
        private readonly IActorRef _queryRootActor;
        private readonly IActorRef _commandRootActor;
        private readonly IActorRef _eventRootActor;
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public CreateModel(IActorRefFactory actorRefFactory, IHostingEnvironment hostingEnvironment)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
            _commandRootActor = actorRefFactory.ActorOf<CommandRootActor>();
            _eventRootActor = actorRefFactory.ActorOf<EventRootActor>();
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            ArticleModel = new ArticleModel
            {
                Date = DateTime.Now
            };
            
            Sections = (await _queryRootActor
                    .Ask<IEnumerable<SectionDetailsRecord>>(new GetSectionList()))
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (ArticleModel.Image != null)
            {
                ArticleModel.ImageUrl = $"~/Uploads/{ArticleModel.Image.FileName}";
                await UploadImage();
            }

            await CreateArticle();

            return RedirectToPage("/Index");
        }
        
        private async Task CreateArticle()
        {
            var result = await _commandRootActor
                .Ask<IdCommandResult>(new AddArticleCommand(ArticleModel.SectionId, ArticleModel.Title, 
                    ArticleModel.Date, ArticleModel.Text, ArticleModel.ImageUrl));

            if (result.Success)
            {
                await _eventRootActor.Ask<CommandResult>(new ArticleAddedEvent(result.Id, ArticleModel.SectionId));
            }
        }
        
        private async Task UploadImage()
        {
            var uploadsDirectoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, ArticleModel.Image.FileName);

            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await ArticleModel.Image.CopyToAsync(fileStream);
            }
        }
    }
}