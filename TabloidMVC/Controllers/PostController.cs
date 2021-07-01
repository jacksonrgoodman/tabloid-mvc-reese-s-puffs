using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System;
using System.Collections.Generic;

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
            int currentUser = GetCurrentUserProfileId();
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
        // GET: Post/Delete/EXAMPLE ID
        public ActionResult Delete(int id)
        {
            Models.Post post = _postRepository.GetPostById(id);

            return View(post);
        }

        // POST: Post/Delete/EXAMPLE ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        // GET: Post/Edit/EXAMPLE ID
        public ActionResult Edit(int id)
        {
            Post post = _postRepository.GetPostById(id);
            List<Category> category = _categoryRepository.GetAll();
            PostEditViewModel vm = new PostEditViewModel()
            { post = post, 
              categories= category
            };
           
           

            return View(vm);
        }

        // POST: Post/Edit/EXAMPLE ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {

                Post currentPost = _postRepository.GetPostById(id);
            List<Category> category = _categoryRepository.GetAll();
            PostEditViewModel vm = new PostEditViewModel()
            {
                post = post,
                categories = category
            };
            try
            {
                post.Id = id;
                _postRepository.UpdatePost(post);

                return RedirectToAction("Details",new { id = vm.post.Id});
            }
            catch (Exception ex)
            {
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
