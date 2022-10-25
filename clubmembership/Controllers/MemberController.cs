using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace clubmembership.Controllers
{
    [Authorize(Roles = "User")]
    public class MemberController : Controller
    {
        private MemberRepository _memberRepository;

        public MemberController(ApplicationDbContext dbContext)
        { 
        _memberRepository=new MemberRepository(dbContext);
        }
        // GET: MemberController
        public ActionResult Index()
        {
            var list = _memberRepository.GetAllMembers();
            return View(list);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            return View("DetailsMember");
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View("CreateMember");
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MemberModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _memberRepository.InsertMember(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMember"); ;
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _memberRepository.GetMemberbyID(id);
            return View("EditMember",model);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _memberRepository.GetMemberbyID(id);
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _memberRepository.UpdateMember(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditMember");
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _memberRepository.GetMemberbyID(id);
            return View("DeleteMember",model);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
