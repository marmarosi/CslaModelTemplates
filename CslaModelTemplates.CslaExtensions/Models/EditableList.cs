using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        where C : EditableModel<C>
    {
        #region ToDto

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

        #endregion

        #region Update

        /// <summary>
        /// Updates an editable collection from the data transfer objects.
        /// </summary>
        /// <typeparam name="D">The type of the data transfer objects.</typeparam>
        /// <param name="list">The list of data transfer objects.</param>
        /// <param name="keyName">The name of the key property.</param>
        public async Task Update<D>(
            List<D> list,
            string keyName
            )
            where D : class
        {
            List<int> indeces = Enumerable.Range(0, list.Count).ToList();
            for (int i = Items.Count - 1; i > -1; i--)
            {
                C item = Items[i];
                long? keyValue = GetValue(item, keyName);
                Predicate<D> match = (D o) => GetValue(o, keyName) == keyValue;
                D dto = list.Find(match);

                if (dto == null)
                    RemoveItem(i);
                else
                {
                    await item.Update(dto);
                    indeces.Remove(list.IndexOf(dto));
                }
            }
            foreach (int index in indeces)
                Items.Add(await (
                    typeof(EditableModel<C>)
                    .GetMethod("Create")
                    .MakeGenericMethod(typeof(D))
                    .Invoke(null, new object[] { this, list[index] })
                    as Task<C>));
        }

        private long? GetValue(
            object something,
            string propertyName
            )
        {
            return something.GetType()
                .GetProperty(propertyName)
                .GetValue(something) as long?;
        }

        #endregion
    }
}
