using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System_UI.ViewModels.Tables
{
    public interface ITableViewModel : IScreen
    {
        public void Save();

        bool isSaved { get; }
    }
}
