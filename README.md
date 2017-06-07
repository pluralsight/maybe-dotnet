# maybe-dotnet
This project provides a `Maybe<T>` `struct` which can be used to help clarify and handle cases when methods may return no value instead of returning `null`.
[This blog post](https://www.pluralsight.com/tech-blog/maybe) explains the motivations for why you would use it.

## Examples
These examples assume that you have an `Account` class and a `repository` for finding and saving accounts.

Creating maybes
```csharp
var some = Maybe<Account>.Some(new Account());
var none = Maybe<Account>.None;
var someOther = account.ToMaybe(); // will be some or none depending on whether the account was null.
```

Checking if the maybe has value
```csharp
var foundAccount = maybeAccount.HasValue;
```

Getting the value from a maybe
```csharp
var account = maybeAccount.ValueOrDefault(new Account());
```

```csharp
var account = maybeAccount.ValueOrThrow(new Exception("No account was found"));
```

Performing an action if there is a value
```csharp
var maybeAccount = repository.Find(accountId);
maybeAccount.IfSome(account =>
{
    account.LastUpdated = DateTimeOffset.UtcNow;
    repository.Save(account);
});
```

Handle the cases where there is some value or there is none 
```csharp
var name = maybeAccount.Case(
    some: account => account.FirstName,
    none: () => "Annonymous");
```

Map a maybe to another type
```csharp
Maybe<string> maybeFirstName = maybeAccount.Map(account => account.FirstName);
Maybe<IList<string>> emails = maybeAccount.Map(account => repository.GetEmailAddresses(account));
```
