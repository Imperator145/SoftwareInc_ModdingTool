using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a advanced ObservableCollection of Generic Items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdvancedList<T> : ObservableCollection<T> where T : class, new()
    {
        #region event
        public new event EventHandler PropertyChanged;
        

        /// <summary>
        /// fires the event
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged([CallerMemberName]string name = "")
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(name));
        }

        #endregion event

        /// <summary>
        /// Internal field
        /// </summary>
        private T current = null;

        /// <summary>
        /// Gets the Current selected Item
        /// </summary>
        public T Current
        {
            get { return current; }
            private set
            {
                current = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Returns the first according Item
        /// </summary>
        /// <param name="p">predicate to Find</param>
        /// <param name="overwriteCurrent">if true the Current Item will be overwritten</param>
        /// <returns></returns>
        public T Find(Func<T, bool> p, bool overwriteCurrent = false)
        {
            var item = this.Where(p).ToArray();
            if (item != null && item.Count() > 0 && overwriteCurrent)
            {
                Current = item[0];
            }
            return item[0];
        }

        /// <summary>
        /// updates the CUrrent Item
        /// </summary>
        /// <param name="newCurrent"></param>
        public void SetCurrent(T newCurrent)
        {
            if (newCurrent != null && Contains(newCurrent))
            {
                Current = newCurrent;
            }
            else
            {
                Current = null;
            }
        }

        /// <summary>
        /// Gets the Current item and creates a new one if its null
        /// </summary>
        public T GetCurrent()
        {
            if (Current == null)
            {
                return New();
            }
            else
                return Current;
        }

        /// <summary>
        /// Sets the Current Item-Property to NULL
        /// </summary>
        public void ClearCurrent()
        {
            Current = null;
        }

        /// <summary>
        /// Removes the CurrentItem from the list and clears the Current Property
        /// </summary>
        public void RemoveCurrent()
        {
            if (current != null)
            {
                Remove(current);
                ClearCurrent();
            }
        }

        /// <summary>
        /// adds a new item and set it as Current
        /// </summary>
        public void New(T newItem)
        {
            Add(newItem);
            Current = newItem;
        }

        /// <summary>
        /// adds a new empty item, set it as Current and return the CUrrent-Item
        /// </summary>
        public T New()
        {
            New(new T());
            return Current;
        }

        /// <summary>
        /// Adds the whole array of items
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (var i in items)
                {
                    Add(i);
                }
            }
        }
    }
}
