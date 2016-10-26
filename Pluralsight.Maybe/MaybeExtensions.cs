using System.Collections.Generic;
using System.Linq;

namespace Pluralsight.Maybe
{
    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value) where T : class
        {
            return value != null
                ? Maybe.Some(value)
                : Maybe<T>.None;
        }

        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct
        {
            return nullable.HasValue
                ? Maybe.Some(nullable.Value)
                : Maybe<T>.None;
        }

        public static Maybe<string> NoneIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s)
                ? Maybe<string>.None
                : Maybe.Some(s);
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> self) where T : class
        {
            return self.FirstOrDefault().ToMaybe();
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T?> self) where T : struct
        {
            return self.FirstOrDefault().ToMaybe();
        }
    }
}