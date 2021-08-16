using Csla;
using Csla.Core;
using Csla.Rules;
using CslaModelTemplates.CslaExtensions.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CslaModelTemplates.CslaExtensions.Models
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
        #region Properties

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

        #endregion

        #region IsValid

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

        #endregion

        #region CollectMessages

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

        #endregion

        #region ToDto

        /// <summary>
        /// Converts the business object to data transfer object.
        /// </summary>
        /// <typeparam name="D">The class of the data transfer object.</typeparam>
        /// <returns>The data transfer object.</returns>
        public D ToDto<D>() where D : class
        {
            Type type = typeof(D);
            D dto = Activator.CreateInstance(type) as D;

            List<IPropertyInfo> cslaProperties = FieldManager.GetRegisteredProperties();
            List<PropertyInfo> dtoProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.Name.StartsWith("__"))
                .ToList();

            foreach (var dtoProperty in dtoProperties)
            {
                var cslaProperty = cslaProperties.Find(pi => pi.Name == dtoProperty.Name);
                if (cslaProperty != null)
                {
                    if (cslaProperty.Type.GetInterface(nameof(IEditableList)) != null)
                    {
                        Type childType = dtoProperty.PropertyType.GenericTypeArguments[0];
                        IEditableList cslaBase = GetProperty(cslaProperty) as IEditableList;
                        object value = cslaProperty.Type
                            .GetMethod("ToDto")
                            .MakeGenericMethod(childType)
                            .Invoke(cslaBase, null);
                        dtoProperty.SetValue(dto, value);
                    }
                    else if (cslaProperty.Type.GetInterface(nameof(IEditableModel)) != null)
                    {
                        Type childType = dtoProperty.PropertyType;
                        IEditableModel cslaBase = GetProperty(cslaProperty) as IEditableModel;
                        object value = cslaProperty.Type
                            .GetMethod("ToDto")
                            .MakeGenericMethod(childType)
                            .Invoke(cslaBase, null);
                        dtoProperty.SetValue(dto, value);
                    }
                    else
                        dtoProperty.SetValue(dto, GetProperty(cslaProperty));
                }
            }

            return dto;
        }

        #endregion

        public virtual void Update<D>(
            D dto
            )
            where D : class
        { }

        public static async Task<T> Create<D>(
            IParent parent,
            D dto
            )
            where D : class
        {
            T item = await Task.Run(() => DataPortal.CreateChild<T>());
            (item as EditableModel<T>).SetParent(parent);
            (item as EditableModel<T>).Update(dto);
            return item;
        }
    }
}
