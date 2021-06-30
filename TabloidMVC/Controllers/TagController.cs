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
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            return View(tags);
        }
        //TODO GET: Tags/AddTagToPost
        public IActionResult ManageTags(int id)
        {
            var vm = new PostTagViewModel();
            vm.Post = new Post();
            vm.Post.Id = id;
            vm.Tags = _tagRepository.GetAllTags();
            return View(vm);
        }
        public ActionResult AddTagToPost(int post, int tag)
        {
            try
            {
                _tagRepository.AddTagToPost(tag, post);
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View();
            }
        }
        //TODO GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }
        //TODO POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }
        //TODO GET: Tags/Edit/*EXAMPLE ID*
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }
        //TODO POST: Tags/Edit/*EXAMPLE ID*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            try
            {
                _tagRepository.UpdateTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }
        // GET: Tags/Delete/EXAMPLE ID
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);

            return View(tag);
        }
        // POST: Tags/Delete/EXAMPLE ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.DeleteTag(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }
    }
    
}
