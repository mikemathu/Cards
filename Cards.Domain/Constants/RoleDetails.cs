namespace Cards.Domain.Constants
{
    public static class RoleDetails
    {
        public const string Admin = "Admin";
        public const string Member = "Member";

        public static Dictionary<string, string> RoleNameToIdMappings { get; } = new Dictionary<string, string>
        {
            { Admin, "Adminf86-5601-41eb-a871-a660b2f0f449" },
            { Member, "Member8a-19f1-430e-aba5-9082dacfa9dd" }
        };
    }
}
