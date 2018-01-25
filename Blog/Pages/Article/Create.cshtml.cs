using System;
using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command.Article;
using Core.CQRS.Command;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ArticleModel ArticleModel { get; set; }
        
        private readonly IActorRef _queryRootActor;
        private readonly IActorRef _commandRootActor;
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public CreateModel(ActorSystem actorSystem, IHostingEnvironment hostingEnvironment)
        {
            _queryRootActor = actorSystem.ActorOf<QueryRootActor>();
            _commandRootActor = actorSystem.ActorOf<CommandRootActor>();
            _hostingEnvironment = hostingEnvironment;
        }
        
        public void OnGet()
        {
            ArticleModel = new ArticleModel
            {
                Date = DateTime.Now
            };
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
            await _commandRootActor
                .Ask<CommandResult>(new AddArticleCommand(ArticleModel.Title, 
                    ArticleModel.Date, ArticleModel.Text, ArticleModel.ImageUrl));
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