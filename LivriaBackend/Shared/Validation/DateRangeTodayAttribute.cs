using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.Extensions.Localization;
using LivriaBackend.Shared.Resources;

namespace LivriaBackend.Shared.Validation
{
    public class DateRangeTodayAttribute : ValidationAttribute
    {
        public string MinimumDate { get; set; }
        
        public string ErrorResourceName { get; set; } = "DateNotInRange";
        public Type ErrorResourceType { get; set; } = typeof(SharedResource);
        
        public DateRangeTodayAttribute(string minimumDate)
        {
            MinimumDate = minimumDate;
        }
        
        public DateRangeTodayAttribute() : this(DateTime.MinValue.ToString("yyyy-MM-dd"))
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateValue)
            {
                DateTime parsedMinDate;
                
                if (string.IsNullOrEmpty(MinimumDate) || !DateTime.TryParseExact(MinimumDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedMinDate))
                {
                    parsedMinDate = DateTime.MinValue;
                }
                
                DateTime maxDate = DateTime.Today;
                
                if (dateValue.Date >= parsedMinDate.Date && dateValue.Date <= maxDate.Date)
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
                                                  (ErrorMessage ?? "The '{0}' field must be a date between {1} and today.");

                    string finalErrorMessage = string.Format(
                        errorMessageTemplate,
                        validationContext.DisplayName ?? validationContext.MemberName,
                        parsedMinDate.ToShortDateString()
                    );

                    return new ValidationResult(finalErrorMessage, new[] { validationContext.MemberName });
                }
            }
            
            return new ValidationResult(ErrorMessage ?? "The field must be a valid date type.");
        }
    }
}