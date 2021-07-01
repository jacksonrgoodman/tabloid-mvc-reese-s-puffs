using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }
        

        public IActionResult UsersIndex()
        {
            var posts = _postRepository.GetAllPostsFromUser(GetCurrentUserProfileId()) ;
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var vm = new PostDetailViewModel();
            vm.Post = new Post();
            vm.AssociatedTags = _postRepository.GetTagByPostId(id);

            vm.Post = _postRepository.GetPublishedPostById(id);
            if (vm.Post == null)
            {
                int userId = GetCurrentUserProfileId();
                vm.Post = _postRepository.GetUserPostById(id, userId);
                if (vm.Post == null)
                {
                    return NotFound();
                }
            }
            return View(vm);
        }

        //TODO GET: Tags/AddTagToPost
        public IActionResult ManageTags(int id)
        {
            var vm = new PostTagViewModel();
            vm.Post = new Post();
            vm.Post.Id = id;
            vm.Tags = _tagRepository.GetAllTags();
            vm.AssociatedTags = _postRepository.GetTagByPostId(id);

            return View(vm);
        }
        public ActionResult AddTagToPost(int post, int tag)
        {
            try
            {
                _tagRepository.AddTagToPost(tag, post);
                
                return RedirectToAction("Details", new { id = post });
            }

            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult RemoveTagFromPost(int post, int tag)
        {
            try
            {
                _tagRepository.RemoveTagFromPost(tag, post);
                
                return RedirectToAction("Details", new { id = post });
            }

            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
