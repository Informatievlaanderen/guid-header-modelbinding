namespace Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.ModelBinding.GuidHeader
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class GuidHeaderModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Guid) && bindingContext.ModelType != typeof(Guid?))
                return Task.CompletedTask;

            if (bindingContext.BindingSource is null || !bindingContext.BindingSource.CanAcceptDataFrom(BindingSource.Header))
                return Task.CompletedTask;

            var headerName = bindingContext.ModelName;

            if (!bindingContext.HttpContext.Request.Headers.TryGetValue(headerName, out var stringValue))
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, stringValue, stringValue);

            // Attempt to parse the guid
            if (Guid.TryParse(stringValue, out var valueAsGuid))
                bindingContext.Result = ModelBindingResult.Success(valueAsGuid);

            return Task.CompletedTask;
        }
    }
}
