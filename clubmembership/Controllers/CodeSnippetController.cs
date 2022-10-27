using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Repository;
using clubmembership.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clubmembership.Controllers
{
    public class CodeSnippetController : Controller
    {
        
        private CodeSnippetRepository _codeSnippetRepository;

        private MemberRepository _memberRepository; 

        public CodeSnippetController(ApplicationDbContext dbContext)
        {
            _memberRepository = new MemberRepository(dbContext);
            _codeSnippetRepository= new CodeSnippetRepository(dbContext);

        }
        // GET: CodeSnippetController
        public ActionResult Index()
        {
            var list = _codeSnippetRepository.GetAllCodeSnippet();
            var viewmodellist = new List<CodeSnippetViewModel>();
            foreach (var codesnippet in list)
            {
                viewmodellist.Add(new CodeSnippetViewModel(codesnippet, _memberRepository));
            }
            
            return View(viewmodellist);
        }

        // GET: CodeSnippetController/Details/5
        public ActionResult Details(Guid id)
        {
            return View("DetailsCodeSnippet");
        }

        // GET: CodeSnippetController/Create
        public ActionResult Create()
        {
            var members = _memberRepository.GetAllMembers();
            var memberList = members.Select(x => new SelectListItem(x.Name, x.IdMember.ToString()));
            ViewBag.MemberList = memberList;
            var model = new CodeSnippetModel();
            model.IdsnippetPreviousVersions= _codeSnippetRepository.GetLatestCodeSnippet().IdcodeSnippet;
            return View("CreateCodeSnippet",model);
        }

        // POST: CodeSnippetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model= new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);
                if (task.Result)
                {
                    _codeSnippetRepository.InsertCodeSnippet(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCodeSnippet");
            }
        }

        // GET: CodeSnippetController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _codeSnippetRepository.GetCodeSnippetbyId(id);
            return View("EditCodeSnippet",model);
        }

        // POST: CodeSnippetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);  
                task.Wait();
                if (task.Result)
                { 
                _codeSnippetRepository.UpdateCodeSnippet(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditCodeSnippet");
            }
        }

        // GET: CodeSnippetController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _codeSnippetRepository.GetCodeSnippetbyId(id);
            return View("DeleteCodeSnippet",model);
        }

        // POST: CodeSnippetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _codeSnippetRepository.DeleteCodeSnippet(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
