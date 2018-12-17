# Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.ModelBinding.GuidHeader

An MVC ModelBinder for GUID type or GUID as string, which also looks in the header.

## Usage

```csharp
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public class FromCommandIdAttribute : Attribute, IModelNameProvider, IBinderTypeProviderMetadata
{
    public BindingSource BindingSource => BindingSource.Header;

    public string Name => "CommandId";

    public Type BinderType => typeof(GuidHeaderModelBinder);
}
```

### Controller

```csharp
public async Task<IActionResult> Post(
            [FromCommandId] Guid? commandId,
            [FromBody] ...,
            CancellationToken cancellationToken)
{
    ...
}
```
