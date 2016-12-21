using SOPerformanceTool.Interfaces;
using SOPerformanceTool.Utilities;
using SOPerformanceTool.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SOPerformanceTool.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MetricsPage : Page, IView
    {
        public MetricsPage()
        {
            this.InitializeComponent();
            (DataContext as MetricsViewModel).View = this as IView;
        }

        public void ExportToExcelFile(string fileName)
        {
            ExcelExportv2.ExportDataGridToExcel(this.dataGrid, fileName);
        }
    }
}
