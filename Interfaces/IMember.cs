using bowling.Models;

namespace bowling.Interfaces
{
    public interface IMember
    {
        string Name { get; set; }
        MemberType MemberType { get; set; }
        string GetMemberDescription();
    }
}
