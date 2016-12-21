using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPerformanceTool.Interfaces
{
    public interface IView
    {
        void ExportToExcelFile(string fileName);
    }
}
