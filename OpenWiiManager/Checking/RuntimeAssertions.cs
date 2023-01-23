using OpenWiiManager.Language.Exceptions;
using System.IO.Compression;

namespace OpenWiiManager.Checking
{
    public class RuntimeAssertions
    {
        public static void True(bool val)
        {
            if (!val)
                throw new RuntimeAssertionFailedException();
        }

        public static void False(bool val)
        {
            True(!val);
        }

        public static void NotNull(object? entry)
        {
            NotEquals(entry, null);
        }

        public static void Null(object? entry)
        {
            AssertEquals(entry, null);
        }
        
        public static void AssertEquals(object? entry, object? value)
        {
            True(entry == value);
        }

        public static void NotEquals(object? entry, object? value)
        {
            True(entry != value);
        }


        public static void True(bool val, string message)
        {
            if (!val)
                throw new RuntimeAssertionFailedException(message);
        }

        public static void False(bool val, string message)
        {
            True(!val, message);
        }

        public static void NotNull(object? entry, string message)
        {
            NotEquals(entry, null, message);
        }

        public static void Null(object? entry, string message)
        {
            AssertEquals(entry, null, message);
        }

        public static void AssertEquals(object? entry, object? value, string message)
        {
            if (entry?.Equals(value) != true && !(entry == null && value == null))
                throw new RuntimeAssertionFailedException(message);
        }

        public static void NotEquals(object? entry, object? value, string message)
        {
            if (entry?.Equals(value) != false)
                throw new RuntimeAssertionFailedException(message);
        }
    }
}