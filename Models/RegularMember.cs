using bowling.Interfaces;

namespace bowling.Models
{
    public class RegularMember : IMember
    {
        public required string Name { get; set; }
        public MemberType MemberType { get; set; }


        public string GetMemberDescription()
        {
            return $"regular member {Name}";
        }
    }
}
