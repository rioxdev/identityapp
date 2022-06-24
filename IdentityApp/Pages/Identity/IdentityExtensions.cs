using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace IdentityApp.Pages.Identity
{
    public static class IdentityExtensions
    {
        public static bool Process(this IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (IdentityError error in result.Errors ?? Enumerable.Empty<IdentityError>())
                modelState.AddModelError(String.Empty, error.Description);

            return result.Succeeded;
        }
    }
}
