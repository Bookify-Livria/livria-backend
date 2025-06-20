using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Localization;
using LivriaBackend.notifications.Domain.Model.ValueObjects;
using LivriaBackend.notifications.Application.Resources;

namespace LivriaBackend.notifications.Application.Validation
{
    public class ValidNotificationTypeAttribute : ValidationAttribute
    {
        public string ErrorResourceName { get; set; } = "InvalidNotificationType"; 
        public Type ErrorResourceType { get; set; } = typeof(DataAnnotations); 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is string typeString)
            {
                if (Enum.TryParse(typeof(ENotificationType), typeString, ignoreCase: true, out object result) && Enum.IsDefined(typeof(ENotificationType), result))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    
                    var localizerFactory = validationContext.GetService(typeof(IStringLocalizerFactory)) as IStringLocalizerFactory;
                    IStringLocalizer localizer = null;

                    if (localizerFactory != null && ErrorResourceType != null)
                    {
                        localizer = localizerFactory.Create(ErrorResourceType);
                    }
                    
                    string errorMessageTemplate = localizer?[ErrorResourceName] ??
                                                  (ErrorMessage ?? "The '{0}' field has an invalid value.");
                    
                    string finalErrorMessage = string.Format(
                        errorMessageTemplate,
                        validationContext.DisplayName ?? validationContext.MemberName
                    );

                    return new ValidationResult(finalErrorMessage, new[] { validationContext.MemberName });
                }
            }
            
            return new ValidationResult(ErrorMessage ?? "The field must be a string.");
        }
    }
}