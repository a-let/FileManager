using System;

namespace FileManager.UI.Interfaces
{
    public interface IFileManagerViewModel
    {
        // TODO: Refactor this to be better
        Action<IFileManagerViewModel> OpenWindow { get; set; }
    }
}