using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
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

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
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
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
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
