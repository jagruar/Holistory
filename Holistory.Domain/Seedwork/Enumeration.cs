using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Holistory.Domain.Seedwork
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration
        {
            var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            bool typeMatches = GetType().Equals(obj.GetType());
            bool valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            return Math.Abs(firstValue.Id - secondValue.Id);
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            return Parse<T, int>(value, "value", item => item.Id == value);
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            return Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().FirstOrDefault(predicate) ?? throw new Exception($"'{value}' is not a valid {description} in {typeof(T)}.");
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public static bool operator ==(Enumeration first, Enumeration second)
        {
            return Equals(first, second);
        }

        public static bool operator !=(Enumeration first, Enumeration second)
        {
            return !Equals(first, second);
        }

        public static implicit operator int(Enumeration enumeration)
        {
            return enumeration.Id;
        }
    }
}
