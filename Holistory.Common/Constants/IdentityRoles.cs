using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Holistory.Common.Constants
{
    public static class IdentityRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<string> GetAll()
        {
            return typeof(IdentityRoles)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(x => x.GetRawConstantValue().ToString())
                .ToList();
        }
    }
}
