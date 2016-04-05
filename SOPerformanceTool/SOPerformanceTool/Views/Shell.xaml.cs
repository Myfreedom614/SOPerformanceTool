using System.Collections.ObjectModel;
using System.Linq;
using Template10.Common;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;

namespace SOPerformanceTool.Views
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-SplitView
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu { get { return Instance.MyHamburgerMenu; } }

        public Shell()
        {
            Instance = this;
            InitializeComponent();
            Loaded += Shell_Loaded;
        }

        private void Shell_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SettingsButton.IsEnabled = false;
        }

        public Shell(INavigationService navigationService)
        {
            Instance = this;
            InitializeComponent();
            SetNavigationService(navigationService);
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            MyHamburgerMenu.NavigationService = navigationService;
        }

        public static void SetBusy(bool busy, string text = null)
        {
            WindowWrapper.Current().Dispatcher.Dispatch(() =>
            {
                Instance.BusyView.BusyText = text;
                Instance.ModalContainer.IsModal = Instance.BusyView.IsBusy = busy;
            });
        }

        public static void SetCurrentProduct(string product)
        {
            ObservableCollection<HamburgerButtonInfo> pbs = Shell.HamburgerMenu.PrimaryButtons as ObservableCollection<HamburgerButtonInfo>;
            if (pbs != null)
            {
                foreach (var pb in pbs)
                {
                    var content = ((HamburgerButtonInfo)pb).Content;
                    if (content != null)
                    {
                        var tb = ((Windows.UI.Xaml.Controls.Panel)content).Children.FirstOrDefault(x => x is Windows.UI.Xaml.Controls.TextBlock);
                        if (tb != null)
                        {
                            if (((Windows.UI.Xaml.Controls.TextBlock)tb).Tag.ToString() == product)
                            {
                                System.Diagnostics.Debug.WriteLine("old-"+pb.IsChecked);
                                pb.IsChecked = true;
                                System.Diagnostics.Debug.WriteLine("new-" + pb.IsChecked);
                            }
                        }
                    }
                }

            }
        }
    }
}

