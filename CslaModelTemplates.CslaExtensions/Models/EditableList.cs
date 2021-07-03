using Csla;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.CslaExtensions.Models
{
    /// <summary>
    /// Wrapper for editable collections to hide server side properties.
    /// </summary>
    /// <typeparam name="T">The type of the business collection.</typeparam>
    /// <typeparam name="C">The type of the business objects in the collection.</typeparam>
    [Serializable]
    public abstract class EditableList<T, C> : BusinessListBase<T, C>, IEditableList
        where T : BusinessListBase<T, C>, IEditableList
        where C : BusinessBase<C>
    {
        /// <summary>
        /// Converts the business collection to data transfer object list.
        /// </summary>
        /// <typeparam name="D">The class of the data transfer objects.</typeparam>
        /// <returns>The list of the data transfer objects.</returns>
        public IList<D> ToDto<D>() where D : class
        {
            Type type = typeof(List<D>);
            IList<D> instance = Activator.CreateInstance(type) as IList<D>;

            foreach (C item in Items)
            {
                D child = item.GetType()
                    .GetMethod("ToDto")
                    .MakeGenericMethod(typeof(D))
                    .Invoke(item, null) as D;
                instance.Add(child);
            }
            return instance;
        }
    }
}
