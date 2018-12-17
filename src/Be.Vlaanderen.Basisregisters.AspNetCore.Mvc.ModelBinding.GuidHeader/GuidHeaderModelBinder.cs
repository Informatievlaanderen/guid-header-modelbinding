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

            if (!bindingContext.BindingSource.CanAcceptDataFrom(BindingSource.Header))
                return Task.CompletedTask;

            var headerName = bindingContext.ModelName;

            if (!bindingContext.HttpContext.Request.Headers.ContainsKey(headerName))
                return Task.CompletedTask;

            var stringValue = bindingContext.HttpContext.Request.Headers[headerName];
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, stringValue, stringValue);

            // Attempt to parse the guid
            if (Guid.TryParse(stringValue, out var valueAsGuid))
                bindingContext.Result = ModelBindingResult.Success(valueAsGuid);

            return Task.CompletedTask;
        }
    }
}
