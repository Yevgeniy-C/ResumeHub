
using Microsoft.AspNetCore.Mvc;
using MyBlog.Middleware;
using ResumeHub.ViewModels;
using ResumeHub.Service;
using ResumeHub.BL.Auth;
using ResumeHub.BL.Profile;
using ResumeHub.ViewMapper;
using ResumeHub.DAL.Models;
using ResumeHub.BL.Data;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ResumeHub.Controllers
{
    [SiteAuthorize()]
    public class ProfilepostsController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IPost post;

        public ProfilepostsController(ICurrentUser currentUser, IPost post) {
            this.currentUser = currentUser;
            this.post = post;

        }

        [HttpGet]
        [Route("/profile/post/{id}")]
        public async Task<IActionResult> Edit(int id) {
            var userId = await currentUser.GetCurrentUserId() ?? 0;            
            PostViewModel viewModel = new PostViewModel();

            if (id != 0) {
                var postModel = await post.GetPost(id);
                if (postModel == null || postModel.UserId != userId)  {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else {
                    viewModel = ViewMapper.PostMapper.MapPostModelToPostViewModel(postModel);
                }
            }
            return View("Edit", viewModel);
        } 

        [HttpGet]
        [Route("/profile/postdata/{id}")]
        public async Task<IActionResult> postdata(int id ) {
            var userId = await currentUser.GetCurrentUserId() ?? 0;            
            PostViewModel viewModel = new PostViewModel();

            if (id != 0) {
                var postModel = await post.GetPost(id);
                if (postModel == null || postModel.UserId != userId)  {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else {
                    viewModel = ViewMapper.PostMapper.MapPostModelToPostViewModel(postModel);
                }
            }
            return new JsonResult(viewModel);
        }

        [HttpPut]
        [Route("/profile/post")]
        public async Task<IActionResult> EditSave([FromBody] PostViewModel postview) {
            var userId = await currentUser.GetCurrentUserId() ?? 0;
            // add new article
            if (ModelState.IsValid && postview.PostId != null) {
                PostModel dbModel = await post.GetPost(postview.PostId ?? 0);
                if (dbModel.UserId != userId) {
                    ModelState.TryAddModelError("Title", "Такое ощущение что тут хакеры");
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new JsonResult(new ErrorsViewModel(ModelState));
                }
            }

            // checking content
            if (postview.PostId == null && postview.ContentItems.Any(m => m.PostContentId != null)) {
                ModelState.TryAddModelError("Title", "nice try :D");
            }
            
            if (postview.PostId != null) {
                var existingContentIds = (await this.post.GetPostItems(postview.PostId ?? 0)).Where(m => m.PostContentId != null).ToDictionary(m => m.PostContentId ?? 0, m => m.PostId);

                if (postview.ContentItems.Any(m => m.PostContentId != null && !existingContentIds.ContainsKey(m.PostContentId ?? 0))) {
                    ModelState.TryAddModelError("Title", "nice try :D");
                }
            }

            if (!ModelState.IsValid) {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new ErrorsViewModel(ModelState));
            }

            // saving
            var newModel = ViewMapper.PostMapper.MapPostViewModelToPostModel(postview);
            newModel.UserId = userId;
            int postId = await post.AddOrUpdate(newModel);
            await post.AddOrUpdateContentItems(ViewMapper.PostMapper.MapPostItemViewModelToPostItemModel(postview.ContentItems, postId));

            return new JsonResult(new {
                id = postId
            });
        }

        [HttpPost]
        [Route("/profile/post/image")]
        public async Task<IActionResult> UploadImage() {
            int? userid = await currentUser.GetCurrentUserId();
            WebFile webfile = new WebFile();
            string filename = webfile.GetWebFilename(userid + "-" + Request.Form.Files[0].FileName, "postimages");
            await webfile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
            return new JsonResult(new { 
                Filename = filename
            });
        }
    }
}