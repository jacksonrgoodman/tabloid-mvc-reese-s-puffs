using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IPostRepository _postRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public SubscriptionController(ISubscriptionRepository subscriptionRepository, IPostRepository postRepository)
        {
            _subscriptionRepo = subscriptionRepository;
            _postRepo = postRepository;
        }

        // POST: SubscriptionController/Create
        public ActionResult Create(int postId)
        {
            try
            {
                Subscription subscription = new Subscription();
                subscription.SubscriberUserProfileId = GetCurrentUserProfileId();
                Post post = new Post();
                post = _postRepo.GetPublishedPostById(postId);
                subscription.ProviderUserProfileId = post.UserProfileId;

                _subscriptionRepo.CreateSubscription(subscription);
                return RedirectToAction("Details", "Post", new { id = postId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", "Post", new { id = postId });
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
