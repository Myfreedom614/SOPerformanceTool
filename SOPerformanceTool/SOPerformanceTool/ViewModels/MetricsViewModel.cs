using Newtonsoft.Json;
using SOPerformanceTool.Models;
using SOPerformanceTool.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace SOPerformanceTool.ViewModels
{
    public class MetricsViewModel : ViewModelBase
    {
        private string APIBase = "/Api/performance/{0}?product={1}&startdate={2}&enddate={3}";
        private string APIVersion;
        public ObservableCollection<MetricsModel> MetricsData { get; set; }

        public string PageHeader { get { return _Product.ToUpper() + " Metrics Data"; } }

        string _Product = string.Empty;
        public string Product { get { return _Product; } set { Set(ref _Product, value); } }

        string _StartDateValue = string.Empty;
        public string StartDateValue { get { return _StartDateValue; } set { Set(ref _StartDateValue, value); } }

        string _EndDateValue = string.Empty;
        public string EndDateValue { get { return _EndDateValue; } set { Set(ref _EndDateValue, value); } }

        string _DateRangeInfo = string.Empty;
        public string DateRangeInfo { get { return _DateRangeInfo; } set { Set(ref _DateRangeInfo, value); } }

        private string _BusyText = "Please wait...";
        public string BusyText
        {
            get { return _BusyText; }
            set { Set(ref _BusyText, value); }
        }
        public void ShowBusy()
        {
            Views.Shell.SetBusy(true, _BusyText);
        }

        public void HideBusy()
        {
            Views.Shell.SetBusy(false);
        }

        public MetricsViewModel()
        {
            MetricsData = new ObservableCollection<MetricsModel>();
            
            var resources = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            APIVersion = resources.GetString("APIVersion");
            var baseurl = resources.GetString("APIBase");
            if (!String.IsNullOrEmpty(baseurl))
            {
                APIBase = baseurl + APIBase;
            }
            else
            {
                throw new Exception("Can't find APIBase url");
            }
        }

        public void ExportToExcel() => ExportToExcelFile();

        private void ExportToExcelFile()
        {
            var xlsExp = new ExcelExport<MetricsModel>(MetricsData);
            xlsExp.ExportToFile(Product + "_metrics_" + StartDateValue.Replace("/", "") + "-" + EndDateValue.Replace("/", ""));
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(nameof(StartDateValue)))
            {
                StartDateValue = state[nameof(StartDateValue)]?.ToString();
                if (state.ContainsKey(nameof(EndDateValue)))
                {
                    EndDateValue = state[nameof(EndDateValue)]?.ToString();
                }
                state.Clear();
            }
            else
            {
                //Value = parameter?.ToString();
                List<Object> paras = parameter as List<Object>;
                Product = paras[0].ToString();

                StartDateValue = Convert.ToDateTime(paras[1]).ToString("MM/dd/yyyy");
                EndDateValue = Convert.ToDateTime(paras[2]).ToString("MM/dd/yyyy");

                DateRangeInfo = string.Format("Showing data from {0} - {1}", StartDateValue, EndDateValue);
            }
            var task = new Task(new Action(async () =>
            {
                ShowBusy();
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    handler.UseDefaultCredentials = true;
                    using (var client = new HttpClient(handler))
                    {
                        //Choose API version
                        client.DefaultRequestHeaders.Add("X-Version", APIVersion);
                        var url = string.Format(APIBase, "metrics", Product, StartDateValue, EndDateValue);
                        var response = await client.GetStringAsync(url);
                        // Parse JSON response.
                        var data = JsonConvert.DeserializeObject<ObservableCollection<MetricsModel>>(response);
                        
                        foreach (var item in data)
                        {
                            MetricsData.Add(new MetricsModel() {
                                Month = item.Month,
                                MonthName = item.MonthName,
                                Year = item.Year,
                                Scenario = item.Scenario,
                                Volume = item.Volume,
                                SolutionRatio = item.SolutionRatio,
                                FCSR = item.FCSR,
                                CCSR = item.CCSR,
                                ReplyWithoutComVolume = item.ReplyWithoutComVolume,
                                OneDayRR = item.OneDayRR,
                                ThreeDayRR = item.ThreeDayRR,
                                OverallRR = item.OverallRR,
                                ThreeDayAR = item.ThreeDayAR,
                                SevenDayAR = item.SevenDayAR,
                                OverallAR = item.OverallAR,
                                AveragetIRT = item.AveragetIRT,
                                NOIR = item.NOIR,
                                ThreeDayHR = item.ThreeDayHR,
                                SevenDayHR = item.SevenDayHR,
                                OverallHR = item.OverallHR,
                                ThreeDayAHR = item.ThreeDayAHR,
                                SevenDayAHR = item.SevenDayAHR,
                                OverallAHR = item.OverallAHR,
                                ThreeDaySDAR = item.ThreeDaySDAR,
                                SevenDaySDAR = item.SevenDaySDAR,
                                OverallSDAR = item.OverallSDAR,
                                ThreeDaySDHR = item.ThreeDaySDHR,
                                SevenDaySDHR = item.SevenDaySDHR,
                                OverallSDHR = item.OverallSDHR,
                                ThreeDaySDAHR = item.ThreeDaySDAHR,
                                SevenDaySDAHR = item.SevenDaySDAHR,
                                OverallSDAHR = item.OverallSDAHR,
                                SDWorkedVol = item.SDWorkedVol,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                await Task.Delay(500);
                HideBusy();

                //if (!string.IsNullOrEmpty(Product)) Shell.SetCurrentProduct(Product);
            }));
            task.RunSynchronously();
            //return base.OnNavigatedToAsync(parameter, mode, state);
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(StartDateValue)] = StartDateValue;
                state[nameof(EndDateValue)] = EndDateValue;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }
    }
}
