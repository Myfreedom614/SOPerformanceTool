using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System;
using SOPerformanceTool.Views;
using SOPerformanceTool.Models;
using Template10.Controls;

namespace SOPerformanceTool.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        //string _Product = string.Empty;
        //public string Product { get { return _Product; } set { Set(ref _Product, value); } }
        //public string PageHeader { get { return GetCurrentProduct().ToUpper() + " Main Page"; } }
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

        #region Commands
        DelegateCommand<int> _SetPaneCommand;
        public DelegateCommand<int> SetPaneCommand
           => _SetPaneCommand ?? (_SetPaneCommand = new DelegateCommand<int>(SetPaneCommandExecute));
        void SetPaneCommandExecute(int param)
        {
            var qm = (QueryMode)param;

            switch (qm)
            {
                case QueryMode.UT:
                    //System.Diagnostics.Debug.WriteLine("UT");
                    GotoUTPage();
                    break;
                case QueryMode.AnswerPoint:
                    //System.Diagnostics.Debug.WriteLine("AnswerPoint");
                    GotoAnswerPointPage();
                    break;
                case QueryMode.Metrics:
                    //System.Diagnostics.Debug.WriteLine("Metrics");
                    GotoMetricsPage();
                    break;
                case QueryMode.OverallMPI:
                    //System.Diagnostics.Debug.WriteLine("OverallMPI");
                    GotoOverallMPI();
                    break;
                case QueryMode.AgentMPI:
                    //System.Diagnostics.Debug.WriteLine("AgentMPI");
                    GotoAgentMPI();
                    break;
            }
        }
        #endregion

        public void TogglePane()
        {
            Shell.HamburgerMenu.IsOpen = !Shell.HamburgerMenu.IsOpen;
        }

        public string GetCurrentProduct()
        {
            var pb = Shell.HamburgerMenu.PrimaryButtons.FirstOrDefault(x => x.IsChecked == true);
            if (pb!=null)
            {
                var content = ((HamburgerButtonInfo)pb).Content;
                if (content != null)
                {
                    var tb = ((Windows.UI.Xaml.Controls.Panel)content).Children.FirstOrDefault(x=>x is Windows.UI.Xaml.Controls.TextBlock);
                    if (tb != null)
                    {
                        return ((Windows.UI.Xaml.Controls.TextBlock)tb).Tag.ToString();
                    }
                }
            }
            else
            {
                var content = ((HamburgerButtonInfo)Shell.HamburgerMenu.PrimaryButtons.FirstOrDefault()).Content;
                if (content != null)
                {
                    var tb = ((Windows.UI.Xaml.Controls.Panel)content).Children.FirstOrDefault(x => x is Windows.UI.Xaml.Controls.TextBlock);
                    if (tb != null)
                    {
                        return ((Windows.UI.Xaml.Controls.TextBlock)tb).Tag.ToString();
                    }
                }
            }
            return null;
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

        public void GotoUTPage() =>
            NavigationService.Navigate(typeof(Views.UTPage), new List<Object>() { GetCurrentProduct(), StartDateValue, EndDateValue });

        public void GotoAnswerPointPage() =>
            NavigationService.Navigate(typeof(Views.AnswerPointPage), new List<Object>() { GetCurrentProduct(), StartDateValue, EndDateValue });

        public void GotoMetricsPage() =>
            NavigationService.Navigate(typeof(Views.MetricsPage), new List<Object>() { GetCurrentProduct(), StartDateValue, EndDateValue });

        public void GotoOverallMPI() =>
            NavigationService.Navigate(typeof(Views.OverallMPIPage), new List<Object>() { GetCurrentProduct(), StartDateValue, EndDateValue });

        public void GotoAgentMPI() =>
            NavigationService.Navigate(typeof(Views.AgentMPIPage), new List<Object>() { GetCurrentProduct(), StartDateValue, EndDateValue });

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

    }
}

