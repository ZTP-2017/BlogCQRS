using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AutoMapper;
using Blog.ReadSide;
using Blog.ReadSide.Model;
using Blog.ReadSide.Query;
using Blog.ViewModels;
using Blog.WriteSide;
using Blog.WriteSide.Command;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public CreateModel(IActorRefFactory actorRefFactory, IHostingEnvironment hostingEnvironment)
        {
            _queryRootActor = actorRefFactory.ActorOf<QueryRootActor>();
            _commandRootActor = actorRefFactory.ActorOf<CommandRootActor>();
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            ArticleModel = new ArticleModel
            {
                Date = DateTime.Now
            };
            
            Sections = (await _queryRootActor
                .Ask<IEnumerable<SectionDetailsRecord>>(new GetSectionListQuery()))
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

            var result = await _commandRootActor
                .Ask<IdCommandResult>(new AddArticleCommand(ArticleModel.SectionId, ArticleModel.Title,
                    ArticleModel.Date, ArticleModel.Text, ArticleModel.ImageUrl));

            return RedirectToPage(result.Success ? "/Index" : "/Error");
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