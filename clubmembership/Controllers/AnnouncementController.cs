using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clubmembership.Controllers
{
    public class AnnouncementController : Controller
    {
        private AnnouncementRepository _announcementRepository;

        public AnnouncementController(ApplicationDbContext dbcontext)
        { 
        _announcementRepository = new AnnouncementRepository(dbcontext);
        }
        
        // GET: AnnouncementController
        public ActionResult Index()
        {
            var list= _announcementRepository.GetAllAnnouncements();
            return View(list);
        }

        // GET: AnnouncementController/Details/5
        public ActionResult Details(Guid id)
        {
            return View("DetailsAnnouncement");
        }

        // GET: AnnouncementController/Create
        public ActionResult Create()
        {
            return View("CreateAnnouncement");
        }

        // POST: AnnouncementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new AnnouncementModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                { 
                _announcementRepository.InsertAnnouncement(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateAnnouncement");
            }
        }

        // GET: AnnouncementController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _announcementRepository.GetAnnouncementbyID(id);
            return View("EditAnnouncement",model);
        }

        // POST: AnnouncementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new AnnouncementModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _announcementRepository.UpdateAnnoucement(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditAnnouncement");
            }
        }

        [Authorize(Roles ="User,Admin")]
        // GET: AnnouncementController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _announcementRepository.GetAnnouncementbyID(id);
            return View("DeleteAnnouncement",model);
        }
        [Authorize(Roles = "User,Admin")]
        // POST: AnnouncementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _announcementRepository.DeleteAnnouncement(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
