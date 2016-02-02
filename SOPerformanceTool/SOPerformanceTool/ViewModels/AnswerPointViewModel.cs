using Newtonsoft.Json;
using SOPerformanceTool.Models;
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
    public class AnswerPointViewModel : ViewModelBase
    {
        private string APIBase = "http://localhost:1982/Api/performance/{0}?startdate={1}&enddate={2}";

        public ObservableCollection<AnswerPointModel> AnswerPointData { get; set; }

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

        public AnswerPointViewModel()
        {
            AnswerPointData = new ObservableCollection<AnswerPointModel>();
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
                List<DateTime> paras = parameter as List<DateTime>;
                StartDateValue = Convert.ToDateTime(paras[0]).ToString("MM/dd/yyyy");
                EndDateValue = Convert.ToDateTime(paras[1]).ToString("MM/dd/yyyy");

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
                        var url = string.Format(APIBase, "opdata", StartDateValue, EndDateValue);
                        var response = await client.GetStringAsync(url);
                        // Parse JSON response.
                        var data = JsonConvert.DeserializeObject<ObservableCollection<AnswerPointModel>>(response);
                        foreach (var item in data)
                        {
                            AnswerPointData.Add(new AnswerPointModel() { Alias = item.Alias, Vote = item.Vote, Answers = item.Answers, Accepted = item.Accepted, Points = item.Points });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                await Task.Delay(500);
                HideBusy();
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
