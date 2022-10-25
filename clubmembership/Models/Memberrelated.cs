namespace clubmembership.Models
{
    public class Memberrelated
    {
        private MemberModel _memberModel;

        private MembershipModel _membershipModel;

        private MembershipTypeModel membershipTypeModel;

        public MemberModel memberModel
        {
            get
            {
                return _memberModel;
            }

            set
            {
                _memberModel = value;
            }
        }
    }
}
