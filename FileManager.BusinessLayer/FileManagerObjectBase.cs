using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using FileManager.BusinessLayer.Interfaces;
using System.Collections.Generic;

namespace FileManager.BusinessLayer
{
    public abstract class FileManagerObjectBase : IChangeTracking, INotifyPropertyChanged
    {
        internal static string _commandText;
        internal static IDictionary<string, object> _paramDict;
        private static readonly ServiceProvider _services = Setup.CreateServices(_commandText, _paramDict);

        internal static readonly IFileManagerDb _fileManagerDb = _services.GetService<IFileManagerDb>();

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