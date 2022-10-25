using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clubmembership.Controllers
{
    public class MembershipController : Controller
    {
        
        private MembershipRepository _membershipRepository;

        private MemberRepository _memberRepository;

        private MembershipTypeRepository _membershipTypeRepository;

        public MembershipController(ApplicationDbContext dbContext)
        { 
        _membershipRepository = new MembershipRepository(dbContext);
        }
        
        // GET: MembershipController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MembershipController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MembershipController/Create
        public ActionResult Create()
        {
            return View("CreateMembership");
        }

        // POST: MembershipController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MembershipModel();
                var task = TryUpdateModelAsync(model);
                if (task.Result)
                { 
                _membershipRepository.InsertMembership(model); 
                }
                return View("View");
            }
            catch
            {
                return View("CreateMembership");
            }
        }

        // GET: MembershipController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MembershipController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: MembershipController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MembershipController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
