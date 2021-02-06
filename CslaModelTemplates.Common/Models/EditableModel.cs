using Csla;
using Csla.Core;
using Csla.Rules;
using CslaModelTemplates.Common.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using ValidationException = CslaModelTemplates.Common.Validations.ValidationException;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Wrapper for editable models to hide server side properties.
    /// </summary>
    /// <remarks>
    /// JsonIgnore is not applied to hidden inherited properties.
    /// </remarks>
    /// <typeparam name="T">The type of the business object.</typeparam>
    [Serializable]
    public abstract class EditableModel<T> : BusinessBase<T>, IEditableModel
        where T : BusinessBase<T>
    {
        [JsonIgnore]
        public new virtual IParent Parent => base.Parent;

        [JsonIgnore]
        public new virtual bool IsNew => base.IsNew;

        [JsonIgnore]
        public new virtual bool IsDeleted => base.IsDeleted;

        [JsonIgnore]
        public override bool IsDirty => base.IsDirty;

        [JsonIgnore]
        public override bool IsSelfDirty => base.IsSelfDirty;

        [JsonIgnore]
        public override bool IsSavable => base.IsSavable;

        [JsonIgnore]
        public new virtual bool IsChild => base.IsChild;

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

        /// <summary>
        /// Gathers and formats broken rule messages.
        /// </summary>
        /// <param name="model">The actual business object to collect the broken rule messsages from.</param>
        /// <param name="prefix">The prefix to add property descriptions on the actual business object.</param>
        /// <param name="messages">The collection point of the formatted messages.</param>
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

        /// <summary>
        /// Converts the business object to data transfer object.
        /// </summary>
        /// <typeparam name="D">The class of the data transfer object.</typeparam>
        /// <returns>The data transfer object.</returns>
        public D ToDto<D>() where D : class
        {
            Type type = typeof(D);
            D instance = Activator.CreateInstance(type) as D;

            List<IPropertyInfo> properties = FieldManager.GetRegisteredProperties();
            List<FieldInfo> fields =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();

            foreach (var field in fields)
            {
                var property = properties.Find(pi => pi.Name == field.Name);
                if (property != null)
                {
                    if (property.Type.GetInterface(nameof(IEditableList)) != null)
                    {
                        Type childType = field.FieldType.GenericTypeArguments[0];
                        IEditableList cslaBase = GetProperty(property) as IEditableList;
                        object value = property.Type
                            .GetMethod("ToDto")
                            .MakeGenericMethod(childType)
                            .Invoke(cslaBase, null);
                        field.SetValue(instance, value);
                    }
                    else if (property.Type.GetInterface(nameof(IEditableModel)) != null)
                    {
                        Type childType = field.FieldType;
                        IEditableModel cslaBase = GetProperty(property) as IEditableModel;
                        object value = property.Type
                            .GetMethod("ToDto")
                            .MakeGenericMethod(childType)
                            .Invoke(cslaBase, null);
                        field.SetValue(instance, value);
                    }
                    else
                        field.SetValue(instance, GetProperty(property));
                }
            }

            return instance;
        }
    }
}
