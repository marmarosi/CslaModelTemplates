using Csla;
using Csla.Core;
using Csla.Rules;
using CslaModelTemplates.Common.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using ValidationException = CslaModelTemplates.Common.Validations.ValidationException;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Wrapper for editable models to hide server side properties.
    /// </summary>
    /// <remarks>
    /// DOES NOT WORK FOR A NewtonSoft.Json "FEATURE".
    /// JsonIgnore is not applied to hidden inherited properties.
    /// </remarks>
    /// <typeparam name="T">The type of the business object.</typeparam>
    [Serializable]
    public abstract class EditableModel<T> : BusinessBase<T>, IEditableModel where T : BusinessBase<T>
    {

        [JsonIgnore]
        protected new IParent Parent => base.Parent;

        [JsonIgnore]
        public new bool IsNew => base.IsNew;

        [JsonIgnore]
        public new bool IsDeleted => base.IsDeleted;

        [JsonIgnore]
        public override bool IsDirty => base.IsDirty;

        [JsonIgnore]
        public override bool IsSelfDirty => base.IsSelfDirty;

        [JsonIgnore]
        public override bool IsSavable => base.IsSavable;

        [JsonIgnore]
        public new bool IsChild => base.IsChild;

        [JsonIgnore]
        public override bool IsSelfValid => base.IsSelfValid;

        [JsonIgnore]
        public override bool IsBusy => base.IsBusy;

        [JsonIgnore]
        public override bool IsSelfBusy => base.IsSelfBusy;

        [JsonIgnore]
        public override BrokenRulesCollection BrokenRulesCollection => base.BrokenRulesCollection;

        [JsonIgnore]
        public override bool IsValid
        {
            get
            {
                if (Parent == null)
                {
                    if (base.IsValid)
                        return true;
                    else
                    {
                        List<ValidationMessage> messages = new List<ValidationMessage>();
                        CollectMessages(this, "", ref messages);
                        throw new ValidationException(messages);
                    }
                }
                else
                    return base.IsValid;
            }
        }
        public void CollectMessages(
            BusinessBase model,
            string prefix,
            ref List<ValidationMessage> messages
            )
        {
            foreach (var brokenRule in BrokenRulesCollection)
                messages.Add(new ValidationMessage(GetType().Name, prefix, brokenRule));

            // Check child objects and collections.
            List<IPropertyInfo> propertyInfos = FieldManager.GetRegisteredProperties().ToList();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Type.GetInterface(nameof(IBusinessBase)) != null)
                {
                    IEditableModel child = (IEditableModel)GetProperty(propertyInfo);
                    child.CollectMessages(
                        (BusinessBase)child,
                        prefix + propertyInfo.Name + ".",
                        ref messages
                        );
                }
                else if (propertyInfo.Type.GetInterface(nameof(IEditableCollection)) != null)
                {
                    var property = GetProperty(propertyInfo);
                    var collection = (IList)property;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        IEditableModel child = (IEditableModel)collection[i];
                        child.CollectMessages(
                            (BusinessBase)child,
                            prefix + propertyInfo.Name + "[" + i + "].",
                            ref messages
                            );
                    }
                }
            }
        }

        //protected string GetMessage(string name)
        //{
        //    return ValidationText.ResourceManager.GetString(name);
        //}

        //protected string GetMessage(string name, object value)
        //{
        //    string text = ValidationText.ResourceManager.GetString(name);
        //    return text.Replace("{1}", value.ToString());
        //}
    }
}
