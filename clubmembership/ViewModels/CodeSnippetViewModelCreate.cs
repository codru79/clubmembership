using clubmembership.Models;
using clubmembership.Repository;

namespace clubmembership.ViewModels
{
    public class CodeSnippetViewModelCreate:CodeSnippetViewModel
    {
        public List<MemberModel> Members { get; set; }

        public CodeSnippetViewModelCreate(CodeSnippetModel model, MemberRepository repository) :base(model,repository)
        {
            
            this.Members = repository.GetAllMembers();
        }
    }
}
