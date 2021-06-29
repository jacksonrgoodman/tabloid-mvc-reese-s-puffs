using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
