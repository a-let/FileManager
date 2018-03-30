using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.Interfaces
{
    public interface IFileManagerViewModel
    {
        Action OpenWindow { get; set; }
    }
}