using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System;

namespace SOPerformanceTool.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                //Value = "Designtime value";
                StartDateValue = DateTime.Parse("2016-01-01");
                EndDateValue = DateTime.Parse("2016-01-31");
            }
            else
            {
                StartDateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                EndDateValue = DateTime.Now;
            }
        }

        //string _Value = string.Empty;
        //public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        DateTime _StartDateValue = DateTime.Now;
        public DateTime StartDateValue { get { return _StartDateValue; } set { Set(ref _StartDateValue, value); } }

        DateTime _EndDateValue = DateTime.Now;
        public DateTime EndDateValue { get { return _EndDateValue; } set { Set(ref _EndDateValue, value); } }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.Any())
            {
                //Value = state[nameof(Value)]?.ToString();
                StartDateValue = Convert.ToDateTime(state[nameof(StartDateValue)]);
                EndDateValue = Convert.ToDateTime(state[nameof(EndDateValue)]);
                state.Clear();
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                //state[nameof(Value)] = Value;
                state[nameof(StartDateValue)] = StartDateValue.ToString();
                state[nameof(EndDateValue)] = EndDateValue.ToString();
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        //public void GotoDetailsPage() =>
        //    NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoUTPage() =>
            NavigationService.Navigate(typeof(Views.UTPage), new List<DateTime>() { StartDateValue, EndDateValue });

        public void GotoAnswerPointPage() =>
            NavigationService.Navigate(typeof(Views.AnswerPointPage), new List<DateTime>() { StartDateValue, EndDateValue });

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

    }
}

