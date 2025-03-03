using bowling.Factories;
using bowling.Interfaces;
using bowling.Models;
using System.Text.Json;

namespace bowling.Repo
{
    public static class FileHandler
    {
        private static string _filePath = Path.Combine(Environment.CurrentDirectory, @"Data\members.json");

        public static void WriteToFile(List<IMember> members)
        {
            string jsonString = JsonSerializer.Serialize<List<IMember>>(members);
            File.WriteAllText(_filePath, jsonString);
        }

        public static List<IMember> ReadFile()
        {
            string jsonString = File.ReadAllText(_filePath);
            List<dynamic> memberData = JsonSerializer.Deserialize<List<dynamic>>(jsonString);

            List<IMember> memberList = new();

            // factory pattern used here to parse existing members
            foreach (JsonElement item in memberData)
            {
                MemberType memberType = Enum.Parse<MemberType>(item.GetProperty("MemberType").ToString());
                IMember tempMember = MemberFactory.Create(item.GetProperty("Name").ToString(), memberType);
                memberList.Add(tempMember);
            }

            return memberList;
        }
    }
}
