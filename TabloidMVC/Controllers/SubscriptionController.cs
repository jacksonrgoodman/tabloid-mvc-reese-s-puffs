using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepo = subscriptionRepository;
        }

        public ActionResult Create()
        {
            return View();
        }
        // POST: SubscriptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subscription subscription)
        {
            try
            {
                _subscriptionRepo.CreateSubscription(subscription);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(subscription);
            }
        }
    }
}
