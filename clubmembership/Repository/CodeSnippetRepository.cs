using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;
using System.Linq;

namespace clubmembership.Repository
{
    public class CodeSnippetRepository
    {
        private readonly ApplicationDbContext _DBContext;

        

        public CodeSnippetRepository()
        { 
           
            _DBContext = new ApplicationDbContext();
        }

        public CodeSnippetRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        public CodeSnippetModel MapDBObjectToModel(CodeSnippet dbobject)
        {
            var model = new CodeSnippetModel();
            if (dbobject != null)
            { 
                model.IdcodeSnippet = dbobject.IdcodeSnippet;
                model.Title=dbobject.Title;
                model.ContentCode=dbobject.ContentCode;
                model.Idmember=dbobject.Idmember;
                model.Revision = dbobject.Revision;
                model.IdsnippetPreviousVersions=dbobject.IdsnippetPreviousVersions;
                model.DateTimeAdded = dbobject.DateTimeAdded;
                model.IsPublished = dbobject.IsPublished;
            }
            return model;
        }

        public CodeSnippet MapModelToDBObject(CodeSnippetModel model)
        {
            var dbobject = new CodeSnippet();
            if (model != null)
            { 
                dbobject.IdcodeSnippet = model.IdcodeSnippet;
                dbobject.Title=model.Title;
                dbobject.ContentCode=model.ContentCode;
                dbobject.Idmember=model.Idmember;
                dbobject.Revision = model.Revision;
                dbobject.IdsnippetPreviousVersions=model.IdsnippetPreviousVersions;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IsPublished = model.IsPublished;
            }
            return dbobject;
        }

        public List<CodeSnippetModel> GetAllCodeSnippet()
        {
            var list = new List<CodeSnippetModel>();
            foreach (var dbobject in _DBContext.CodeSnippets)
            { 
            list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }

        public CodeSnippetModel GetCodeSnippetbyId(Guid id)
        {
            return MapDBObjectToModel(_DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == id));
        }

        public CodeSnippetModel GetLatestCodeSnippet()
        {

           return MapDBObjectToModel(_DBContext.CodeSnippets.OrderByDescending(x => x.DateTimeAdded).FirstOrDefault());

        }

        public void InsertCodeSnippet(CodeSnippetModel model)
        { 
            model.IdcodeSnippet= Guid.NewGuid();
            model.DateTimeAdded = DateTime.Now;
            _DBContext.CodeSnippets.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();

        }

        public void UpdateCodeSnippet(CodeSnippetModel model)
        {
            var dbobject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == model.IdcodeSnippet);
            if (dbobject != null)
            { 
                dbobject.IdcodeSnippet = model.IdcodeSnippet;
                dbobject.Title= model.Title;
                dbobject.ContentCode=model.ContentCode;
                dbobject.Idmember = model.Idmember;
                dbobject.Revision = model.Revision;
                dbobject.IdsnippetPreviousVersions = model.IdsnippetPreviousVersions;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IsPublished = model.IsPublished;
            }
        }

        public void DeleteCodeSnippet(Guid id)
        {
            var dbobject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == id);
            _DBContext.CodeSnippets.Remove(dbobject);
            _DBContext.SaveChanges();
        }


    }
}
