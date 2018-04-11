using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.BusinessLayer
{
    public abstract class FileManagerObjectBase : IChangeTracking, INotifyPropertyChanged
    {
        private bool _isChanged = false;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if(_isChanged != value)
                {
                    _isChanged = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public void AcceptChanges()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(string name, ref T oldValue, T newValue) where T : IEquatable<T>
        {
            if(oldValue == null || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                NotifyPropertyChanged(name);
                IsChanged = true;
                return true;
            }

            IsChanged = false;
            return false;
        }
    }
}