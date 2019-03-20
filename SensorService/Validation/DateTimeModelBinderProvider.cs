using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SensorService.Validation
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType)
            {
                if ((context.Metadata.ModelType == typeof(DateTime))||
                    (context.Metadata.IsNullableValueType && context.Metadata.UnderlyingOrModelType == typeof(DateTime)))
                {
                    return new DateTimeModelBinder(context.Metadata.ModelType);
                }
            }

            return null;
        }
    }
}
