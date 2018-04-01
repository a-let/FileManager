using System;

namespace FileManager.UI.Interfaces
{
    public interface IFileManagerViewModel
    {
        Action<IFileManagerViewModel> OpenWindow { get; set; }
    }
}