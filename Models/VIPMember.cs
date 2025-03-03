using bowling.Interfaces;

namespace bowling.Models
{
    public class VIPMember : IMember
    {
        public required string Name { get; set; }
        public MemberType MemberType { get; set; }


        public string GetMemberDescription()
        {
            return $"VIP member {Name}";
        }
    }
}
