using Microsoft.AspNetCore.Http;
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
    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userRepo;
        private readonly IUserTypeRepository _userTypeRepo;

        public UserProfileController(IUserProfileRepository UserProfileRepository, IUserTypeRepository UserTypeRepository)
        {
            _userRepo = UserProfileRepository;
            _userTypeRepo = UserTypeRepository;
        }
        // GET: UserProfile
        public ActionResult Index()
        {
            List<UserProfile> users = _userRepo.GetAllUsers();
            return View(users);
        }
        public ActionResult DeactivateUsers()
        {
            List<UserProfile> deactivated = _userRepo.GetDeactivated();

            return View(deactivated);


        }

        // GET: UserProfile/Details/5
        public ActionResult Details(int id)
        {
            UserProfile user = _userRepo.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            UserProfile user = _userRepo.GetUsersById(id);
            List<UserType> userTypes = _userTypeRepo.GetAll();

            UserFormViewModel vm = new UserFormViewModel()
            {
                UserProfile = user,
                UserTypes = userTypes
            };

            return View(vm);
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile userProfile)
        {
            try
            {
                _userRepo.UpdateUser(userProfile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(userProfile);
            }
        }

        //// GET: UserProfile/Create
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //// POST: UserProfile/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(UserProfile user)
        //{
        //    try
        //    {
        //        _userRepo.Register(user);
        //        return RedirectToAction("Index");
        //    }
        //    catch(Exception)
        //    {
        //        return View(user);
        //    }
        //}

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
            catch (Exception)
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
            if (user.UserTypeId == 1)
            {
                int admin = _userRepo.Admin();
                if (admin > 1)
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
                else
                {
                    return Content("You cannot delete the only admin!");
                }
            }
            else
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




        }

        // GET: UserProfile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();

        }

        // POST: UserProfile/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserProfile user)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
    }
}
