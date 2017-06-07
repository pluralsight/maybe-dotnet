using System;
using NUnit.Specifications;
using Should;

namespace Pluralsight.Maybe.Specs
{
    public class When_calling_some_with_a_null_value : ContextSpecification
    {
        Because of = () => result = Catch.Exception(() => Maybe<string>.Some(null));

        It should_throw_an_invalid_operation_exception = () => result.ShouldBeType<InvalidOperationException>();

        static Exception result;
    }

    public class When_getting_the_value_and_there_is_a_value : ContextSpecification
    {
        Because of = () => result = maybe.Value;

        It should_return_the_value = () => result.ShouldEqual("hi");

        static Maybe<string> maybe = Maybe.Some("hi");
        static string result;
    }

    public class When_getting_the_value_and_there_is_NOT_a_value : ContextSpecification
    {
        Because of = () => result = Catch.Exception(() => { var x = maybe.Value; });

        It should_throw_an_invalid_operation_exception = () => result.ShouldBeType<InvalidOperationException>();

        static Maybe<string> maybe = Maybe<string>.None;
        static Exception result;
    }

    public class When_calling_case_and_there_is_a_value : ContextSpecification
    {
        Because of = () => result = maybe.Case(x => x + "!", () => "none");

        It should_return_the_correct_value = () => result.ShouldEqual("hi!");

        static Maybe<string> maybe = Maybe.Some("hi");
        static string result;
    }

    public class When_calling_case_and_there_is_NOT_a_value : ContextSpecification
    {
        Because of = () => result = maybe.Case(x => x + "!", () => "none");

        It should_return_the_correct_value = () => result.ShouldEqual("none");

        static Maybe<string> maybe = Maybe<string>.None;
        static string result;
    }

    public class When_converting_null_to_maybe : ContextSpecification
    {
        Because of = () => result = value.ToMaybe();

        It should_NOT_have_a_value = () => result.HasValue.ShouldBeFalse();

        static string value = null;
        static Maybe<string> result;
    }

    public class When_converting_a_value_to_maybe : ContextSpecification
    {
        Because of = () => result = value.ToMaybe();

        It should_have_a_value = () => result.HasValue.ShouldBeTrue();

        static string value = "some-value";
        static Maybe<string> result;
    }

    public class When_converting_null_nullable_to_maybe : ContextSpecification
    {
        Because of = () => result = value.ToMaybe();

        It should_NOT_have_a_value = () => result.HasValue.ShouldBeFalse();

        static int? value = null;
        static Maybe<int> result;
    }

    public class When_converting_a_nullable_value_to_maybe : ContextSpecification
    {
        Because of = () => result = value.ToMaybe();

        It should_have_a_value = () => result.HasValue.ShouldBeTrue();

        static int? value = 23;
        static Maybe<int> result;
    }

    public class When_calling_ValueOrDefault_and_there_is_a_value : ContextSpecification
    {
        Because of = () => result = maybe.ValueOrDefault("boo!");

        It should_return_the_value = () => result.ShouldEqual("hi");

        static Maybe<string> maybe = Maybe.Some("hi");
        static string result;
    }

    public class When_calling_ValueOrDefault_and_there_is_NOT_a_value : ContextSpecification
    {
        Because of = () => result = maybe.ValueOrDefault("boo!");

        It should_return_the_value = () => result.ShouldEqual("boo!");

        static Maybe<string> maybe = Maybe<string>.None;
        static string result;
    }

    public class When_calling_ValueOrDefault_and_the_Maybe_is_NOT_initialized : ContextSpecification
    {
        Because of = () => result = maybe.ValueOrDefault("boo!");

        It should_return_the_value = () => result.ShouldEqual("boo!");

        static Maybe<string> maybe;
        static string result;
    }

    public class When_mapping_some : ContextSpecification
    {
        Because of = () => result = maybe.Map(x => x.ToUpper());

        It should_map_the_value = () => result.Value.ShouldEqual("HI");

        static Maybe<string> result;
        static Maybe<string> maybe = Maybe.Some("hi");
    }

    public class When_mapping_none : ContextSpecification
    {
        Because of = () => result = maybe.Map(x => x.ToUpper());

        It should_map_the_value = () => result.HasValue.ShouldBeFalse();

        static Maybe<string> result;
        static Maybe<string> maybe = Maybe<string>.None;
    }

    public class When_mapping_some_with_a_function_that_returns_a_maybe : ContextSpecification
    {
        Because of = () => result = maybe.Map(x => x.ToUpper().ToMaybe());

        It should_return_some = () => result.Value.ShouldEqual("HI");

        static Maybe<string> result;
        static Maybe<string> maybe = Maybe.Some("hi");
    }

    public class When_mapping_none_with_a_function_that_returns_a_maybe : ContextSpecification
    {
        Because of = () => result = maybe.Map(x => x.ToUpper().ToMaybe());

        It should_return_some = () => result.HasValue.ShouldBeFalse();

        static Maybe<string> result;
        static Maybe<string> maybe = Maybe<string>.None;
    }

    public class When_calling_if_some_and_there_are_some : ContextSpecification
    {
        Because of = () => Maybe.Some("yay").IfSome(x => result = x.ToUpper());

        It should_have_run_the_function = () => result.ShouldEqual("YAY");

        static string result = null;
    }

    public class When_calling_if_some_and_there_are_none : ContextSpecification
    {
        Because of = () => Maybe<string>.None.IfSome(x => result = x.ToUpper());

        It should_have_run_the_function = () => result.ShouldBeNull();

        static string result = null;
    }

    public class When_calling_value_or_throw_and_there_is_a_value : ContextSpecification
    {
        Because of = () => value = Maybe.Some(1).ValueOrThrow(new Exception());

        It should_return_the_value = () => value.ShouldEqual(1);

        static int value;
    }

    public class When_calling_value_or_throw_and_there_is_no_value : ContextSpecification
    {
        Because of = () => thrownException = Catch.Exception(() => Maybe<int>.None.ValueOrThrow(exception));

        It should_throw = () => thrownException.ShouldBeSameAs(exception);

        static Exception exception = new Exception();
        static Exception thrownException;
    }
}