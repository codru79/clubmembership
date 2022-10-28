using clubmembership.Models;
using clubmembership.Repository;

namespace clubmembership.ViewModels
{
    public class CodeSnippetViewModel
    {
        public Guid IdcodeSnippet { get; set; }
        public string Title { get; set; } = null!;
        public string ContentCode { get; set; } = null!;
       // public Guid Idmember { get; set; }
        //adaug MemberName
        public string MemberName { get; set; }

        public List<MemberModel> Members { get; set; }
        public int Revision { get; set; }
        //public Guid? IdsnippetPreviousVersions { get; set; }

        //decorators for datetime type fields 
        //[DisplayFormat(DataFormatString = "{0:d}")]
        //[DataType(DataType.Date)]
        public DateTime DateTimeAdded { get; set; }
        public bool IsPublished { get; set; }

        public CodeSnippetViewModel(CodeSnippetModel model, MemberRepository repository)
        {
            this.IdcodeSnippet = model.IdcodeSnippet;
            this.Title = model.Title;
            this.ContentCode = model.ContentCode;
            //this.Idmember = model.Idmember;
            this.Revision = model.Revision;
           // this.IdsnippetPreviousVersions = model.IdsnippetPreviousVersions;
            this.DateTimeAdded = model.DateTimeAdded;
            this.IsPublished = model.IsPublished;
            var member = repository.GetMemberbyID(model.Idmember);
            this.MemberName = member.Name;
            this.Members = repository.GetAllMembers();
        }
    }
}
