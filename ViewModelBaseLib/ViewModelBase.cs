using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ViewModelBaseLib.ViewModelBase
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region IDataErrorInfo Infrustructure

        public virtual string this[string columnName] => throw new NotImplementedException();

        public virtual string Error => throw new NotImplementedException();

        #endregion

        #region On Property Changed Infrastructure
        /// <summary>
        /// When it fires the View gets a signal to update
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Thread safe Invoker of the Property Changed Event
        /// </summary>
        /// <param name="Propertyname"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string Propertyname = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);

            temp?.Invoke(this, new PropertyChangedEventArgs(Propertyname));
        }

        /// <summary>
        /// Sets the field with a value of the propery if the field is not null or 
        /// field is not equal to value in the Property. Property changed event will be called then.
        /// </summary>
        /// <typeparam name="T">Type of a field or value</typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual bool SetProperty<T>(ref T field, T value, string PropertyName)
        {
            if (field == null)
                throw new ArgumentNullException("Suppose you forget to initialize field!");

            if (Equals(field, value))
                return false;
            else
            {
                field = value;
                OnPropertyChanged(PropertyName);
                return true;
            }
        }

        #endregion

        #region Fields

        protected bool[] m_ValidArray;

        #endregion

        #region Methods
        /// <summary>
        /// Checks Valid array. True if all the values in range (start to end) are true
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool CheckValidArray(int start, int end)
        {
            if (m_ValidArray == null)
                throw new NullReferenceException("Validation array wasn't initialized!");

            if (start < 0 || end < 0)
                throw new ArgumentOutOfRangeException("Index of the element in ValidationArray can't be lower than 0!");

            if (end > m_ValidArray.Length - 1)
                throw new ArgumentOutOfRangeException("end parametr is ou of range!");

            for (int i = start; i <= end; i++)            
                if (!m_ValidArray[i])
                    return false;
            
            return true;
        }
                

        #endregion
    }
}
