using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Dwp.Adep.Ucb.ResourceLibrary;

namespace UcbWeb.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class CompareDate : ValidationAttribute, IClientValidatable
    {
        private string defaultErrorMessage = Resources.VAL_DATE_BEFORE;
        private string defaultEqualErrorMessage = Resources.VAL_DATE_BEFORE_EQUAL;

        private string defaultAfterErrorMessage = Resources.VAL_DATE_AFTER;
        private string defaultEqualAfterErrorMessage = Resources.VAL_DATE_AFTER_EQUAL;

        private readonly string testedPropertyName;
        private readonly bool allowEqualDates;
        private readonly bool isDateBefore;
        
        private readonly string uniqueSuffix;
        public CompareDate(string testedPropertyName, bool allowEqualDates = false, bool isDateBefore = true, string uniqueSuffix = "")
        {
            this.testedPropertyName = testedPropertyName;
            this.allowEqualDates = allowEqualDates;
            this.isDateBefore = isDateBefore;
            this.uniqueSuffix = uniqueSuffix;
        }

        //To avoid multiple rules with same name
        public Dictionary<string, int> countPerField = null;

        private object _typeId = new object();
        public override object TypeId
        {
            get
            {
                return this._typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {

            return base.FormatErrorMessage(name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(this.testedPropertyName);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", this.testedPropertyName));
            }
            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);
            if (value == null || !(value is DateTime))
            {
                return ValidationResult.Success;
            }
            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
            {
                return ValidationResult.Success;
            }
            // Compare values 

            if (isDateBefore)
            {

                if ((DateTime)value <= (DateTime)propertyTestedValue)
                {
                    if (this.allowEqualDates)
                    {
                        return ValidationResult.Success;
                    }
                    if ((DateTime)value < (DateTime)propertyTestedValue)
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                if ((DateTime)value >= (DateTime)propertyTestedValue)
                {
                    if (this.allowEqualDates)
                    {
                        return ValidationResult.Success;
                    }
                    if ((DateTime)value > (DateTime)propertyTestedValue)
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            //NEED TO GET THE DISPLAY NAME FOR THE OTHER PROPERTY
            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => validationContext.ObjectInstance, validationContext.ObjectType, this.testedPropertyName);
           
            //***************************************************
            if (isDateBefore)
            {

                if (!allowEqualDates)
                {
                    return new ValidationResult(String.Format(defaultErrorMessage, validationContext.DisplayName, otherMetaData.DisplayName));
                }
                else
                {
                    return new ValidationResult(String.Format(defaultEqualErrorMessage, validationContext.DisplayName, otherMetaData.DisplayName));
                }
            }
            else
            {
                if (!allowEqualDates)
                {
                    return new ValidationResult(String.Format(defaultAfterErrorMessage, validationContext.DisplayName, otherMetaData.DisplayName));
                }
                else
                {
                    return new ValidationResult(String.Format(defaultEqualAfterErrorMessage, validationContext.DisplayName, otherMetaData.DisplayName));
                }
         
            }
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = "";
                if (isDateBefore)
                {
                    errorMessage = allowEqualDates ? String.Format(defaultEqualErrorMessage, metadata.DisplayName, GetDisplayNameForProperty(this.testedPropertyName, metadata)) : String.Format(defaultErrorMessage, metadata.DisplayName, GetDisplayNameForProperty(this.testedPropertyName, metadata)); // this.ErrorMessageString,
                }
                else
                {
                    errorMessage = allowEqualDates ? String.Format(defaultEqualAfterErrorMessage, metadata.DisplayName, GetDisplayNameForProperty(this.testedPropertyName, metadata)) : String.Format(defaultAfterErrorMessage, metadata.DisplayName, GetDisplayNameForProperty(this.testedPropertyName, metadata)); // this.ErrorMessageString,
                }


            var rule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "comparedate" + uniqueSuffix
            };
            rule.ValidationParameters["propertytested"] = this.testedPropertyName;
            rule.ValidationParameters["allowequaldates"] = this.allowEqualDates;
            rule.ValidationParameters["isdatebefore"] = this.isDateBefore;
            yield return rule;
        }
        

        //Private method to get display name for other property from metadata object
        private string GetDisplayNameForProperty(string propertyName,ModelMetadata metadata)
        {
            Type type = Type.GetType(metadata.ContainerType.FullName);
            var model = Activator.CreateInstance(type);

            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => model, type, propertyName);
            return otherMetaData.DisplayName;
        }

    }
    
}