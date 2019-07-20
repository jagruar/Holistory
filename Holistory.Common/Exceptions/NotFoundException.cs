using System;

namespace Holistory.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string valueName)
            :base(valueName)
        {
        }

        public static void ThrowIfNull<T>(T valueToValidate, string valueName)
           where T : class
        {
            if (valueToValidate == null)
            {
                throw new NotFoundException(valueName);
            }
        }
    }
}
