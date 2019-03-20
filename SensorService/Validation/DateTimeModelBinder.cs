using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SensorService.Validation
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly TypeConverter typeConverter;

        public DateTimeModelBinder(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            typeConverter = TypeDescriptor.GetConverter(type);
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    bindingContext.ModelMetadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(valueProviderResult.ToString())
                );

                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            try
            {
                var value = valueProviderResult.FirstValue;

                object model = null;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    model = new DateTimeConverter().ConvertFrom(
                        context: null,
                        culture: valueProviderResult.Culture,
                        value: value
                    );
                }

                if (bindingContext.ModelType == typeof(string))
                {
                    var modelAsString = model as string;
                    if (bindingContext.ModelMetadata.ConvertEmptyStringToNull &&
                        string.IsNullOrWhiteSpace(modelAsString))
                    {
                        model = null;
                    }
                }

                if (model == null && !bindingContext.ModelMetadata.IsReferenceOrNullableType)
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        bindingContext.ModelMetadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(valueProviderResult.ToString())
                    );

                    return Task.CompletedTask;
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                var isFormatException = e is FormatException;
                if (!isFormatException && e.InnerException != null)
                {
                    e = ExceptionDispatchInfo.Capture(e.InnerException).SourceException;
                }

                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    e,
                    bindingContext.ModelMetadata
                );

                return Task.CompletedTask;
            }
        }
    }
}
