﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userRepo;

        public UserProfileController(IUserProfileRepository UserProfileRepository)
        {
            _userRepo = UserProfileRepository;
         }
        // GET: UserProfile
        public ActionResult Index()
        {
            List<UserProfile> users = _userRepo.GetAllUsers();
            return View(users);
        }

        // GET: UserProfile/Details/5
        public ActionResult Details(int id)
        {
            UserProfile user = _userRepo.GetUsersById(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfile/Edit/5
        public ActionResult Reactivate(int id)
        {
            UserProfile user = _userRepo.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(int id, UserProfile user)
        {
            try
            {
                _userRepo.ReactivateUser(user);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(user);
            }
        }

        public ActionResult Deactivate(int id)
        {
            UserProfile user = _userRepo.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id, UserProfile user)
        {
            try
            {
                _userRepo.DeactivateUser(user);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(user);
            }
        }

        // GET: UserProfile/Delete/5
        public ActionResult Delete(int id)
        {
                return View();
                    
        }

        // POST: UserProfile/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,UserProfile user)
        {
            try
            {
               
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(user);
            }
        }
    }
}
