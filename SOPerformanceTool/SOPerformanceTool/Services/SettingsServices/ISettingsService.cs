using System;
using Windows.UI.Xaml;

namespace SOPerformanceTool.Services.SettingsServices
{
    public interface ISettingsService
    {
        bool UseShellBackButton { get; set; }
        ApplicationTheme AppTheme { get; set; }
        TimeSpan CacheMaxDuration { get; set; }
    }
}
