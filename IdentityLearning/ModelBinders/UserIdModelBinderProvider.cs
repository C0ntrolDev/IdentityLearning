﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityLearning.API.ModelBinders
{
    public class UserIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(long) && context.Metadata.BinderModelName == "UserIdModelBinder")
            {
                return new UserIdModelBinder();
            }

            return null;
        }
    }
}
