using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace SOPerformanceTool.Utilities
{
    /// <summary>
    /// This class is based on the CSVExport class in https://code.msdn.microsoft.com/windowsapps/Export-To-CSV-Sample-b31b50cf
    /// The supported excel extension is .xls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelExport<T> where T : class
    {
        private const string TabSeparator = "\t";

        public ObservableCollection<T> Objects;

        public ExcelExport(ObservableCollection<T> objects)
        {
            Objects = objects;
        }

        public string Export()
        {
            return Export(true);
        }

        public string Export(bool includeHeaderLine)
        {

            var sb = new StringBuilder();

            //Get properties using reflection.
            var propertyInfos = typeof(T).GetTypeInfo();

            if (includeHeaderLine)
            {
                //Add header line.
                foreach (var propertyInfo in propertyInfos.DeclaredProperties)
                {
                    sb.Append(propertyInfo.Name).Append(TabSeparator);
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            //Add value for each property.
            foreach (T obj in Objects)
            {
                foreach (var propertyInfo in propertyInfos.DeclaredProperties)
                {
                    sb.Append(MakeValueCsvFriendly(propertyInfo.GetValue(obj, null))).Append(TabSeparator);
                }

                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            return sb.ToString();
        }

        //Export to a file.
        public async void ExportToFile(string filename)
        {
            try
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("Excel file", new List<string>() { ".xls" });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = filename;

                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    await FileIO.WriteTextAsync(file, Export());
                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == FileUpdateStatus.Complete)
                    {
                        var messageDialog = new Windows.UI.Popups.MessageDialog
                           ("File \""+file.Name + "\" was saved");
                        await messageDialog.ShowAsync();
                        System.Diagnostics.Debug.WriteLine(file.Name + " was saved");
                    }
                    else
                    {
                        var messageDialog = new Windows.UI.Popups.MessageDialog
                             ("File \"" + file.Name + "\" couldn't be saved");
                        await messageDialog.ShowAsync();
                        System.Diagnostics.Debug.WriteLine(file.Name + " couldn't be saved");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Operation cancelled");
                }
            } catch(Exception ex)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog
                            ("Failed to export to excel file." +
                            Environment.NewLine + Environment.NewLine + "Message: " + ex.Message);
                await messageDialog.ShowAsync();
            }
        }

        //Export as binary data.
        public byte[] ExportToBytes()
        {
            return Encoding.UTF8.GetBytes(Export());
        }

        //Get value of field.
        private string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";

            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();

            if (output.Contains(",") || output.Contains("\""))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;
        }
    }
    
    public static class ExcelExportv2
    {
        /// <summary>
        /// Method is export the SfDataGrid to Excel. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static async void ExportDataGridToExcel(SfDataGrid sfGrid, string fileName)
        {
            ExcelEngine excelEngine = null;
            ExcelExportingOptions options = new ExcelExportingOptions()
            {
                AllowOutlining = true,
                ExcelVersion = ExcelVersion.Excel2007,
                ExportingEventHandler = exportingHandler,
            };

            options.CellsExportingEventHandler = CellExportingHandler;
            excelEngine = sfGrid.ExportToExcel(sfGrid.View, options);

            var workBook = excelEngine.Excel.Workbooks[0];

            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop,
                SuggestedFileName = fileName
            };

            if (workBook.Version == ExcelVersion.Excel97to2003)
            {
                savePicker.FileTypeChoices.Add("Excel File (.xls)", new List<string>() { ".xls" });
            }
            else
            {
                savePicker.FileTypeChoices.Add("Excel File (.xlsx)", new List<string>() { ".xlsx" });
            }

            var storageFile = await savePicker.PickSaveFileAsync();

            if (storageFile != null)
            {
                await workBook.SaveAsAsync(storageFile);

                var msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                var yesCmd = new UICommand("Yes");
                var noCmd = new UICommand("No");
                msgDialog.Commands.Add(yesCmd);
                msgDialog.Commands.Add(noCmd);
                var cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(storageFile);
                }
            }
            excelEngine.Dispose();
        }

        private static void CellExportingHandler(object sender, GridCellExcelExportingEventArgs args)
        {
            args.Range.CellStyle.Font.FontName = "Segoe UI";
        }

        private static void exportingHandler(object sender, GridExcelExportingEventArgs e)
        {
            if (e.CellType == ExportCellType.HeaderCell)
            {
                e.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.LightBlue);
                e.CellStyle.FontInfo.Bold = true;
            }
            else if (e.CellType == ExportCellType.GroupCaptionCell)
            {
                e.CellStyle.ForeGroundBrush = new SolidColorBrush(Colors.LightYellow);
            }
            else if (e.CellType == ExportCellType.GroupSummaryCell)
            {
                e.CellStyle.BackGroundBrush = new SolidColorBrush(Colors.LightGray);
            }
            e.CellStyle.FontInfo.FontName = "Segoe UI";
            e.Handled = true;
        }
    }
}
