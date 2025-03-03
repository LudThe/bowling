using bowling.Interfaces;
using bowling.Models;

namespace bowling.Factories
{
    public static class MemberFactory
    {
        public static IMember Create(string name, MemberType memberType)
        {
            switch (memberType)
            {
                case MemberType.Regular:
                    return new RegularMember()
                    {
                        Name = name,
                        MemberType = memberType
                    };

                case MemberType.VIP:
                    return new VIPMember()
                    {
                        Name = name,
                        MemberType = memberType
                    };

                default:
                    throw new ArgumentException("No such member type");
            }
        }
    }
}
