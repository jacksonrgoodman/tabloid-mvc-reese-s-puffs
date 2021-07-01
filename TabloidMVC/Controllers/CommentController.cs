using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserProfileRepository userProfileRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            List<Comments> comments = _commentRepository.GetCommentsByPostId(id);
           

            CommentViewModel vm = new CommentViewModel()
            {
                Post = post,
                Comments = comments,
               
            };
            
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var comments = _commentRepository.GetCommentsByPostId(id);
            return View(comments);
        }

        public IActionResult Create(int id)
        {
            var vm = new CommentViewModel();
            vm.Post = new Post();
            vm.Post.Id = id;
            vm.Comments = _commentRepository.GetCommentsByPostId(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CommentViewModel vm)
        {
            try
            {
                vm.Comment.CreateDateTime = DateAndTime.Now;

                vm.Comment.UserProfileId = GetCurrentUserProfileId();
                vm.Comment.PostId = vm.Post.Id;
                _commentRepository.AddComment(vm.Comment);
                return RedirectToAction("Index", new { id = vm.Post.Id });
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

        //Get: comment/delete
        public ActionResult Delete(int id)
        {
            Comments comment = _commentRepository.GetCommentById(id);
            return View(comment);
        }

       
        // POST: Comment/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Comments comment)
        {
            try
            {

                //vm.Comment.UserProfileId = GetCurrentUserProfileId();
                //vm.Comment.Id = vm.Post.Id;
                Comments returnedComment = _commentRepository.GetCommentById(comment.Id);
                _commentRepository.DeleteComment(comment.Id);

                return RedirectToAction("Index", new { id = returnedComment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }

        //Get: comment/edit
        public ActionResult Edit(int id)
        {
            Comments comment = _commentRepository.GetCommentById(id);
            return View(comment);
        }


        // POST: Comment/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comments comment)  
            {
            try
            {
                //Comments returnedComment = _commentRepository.GetCommentById(comment.Id);
                _commentRepository.UpdateComment(comment);

                return RedirectToAction("Index", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }
    }
}
