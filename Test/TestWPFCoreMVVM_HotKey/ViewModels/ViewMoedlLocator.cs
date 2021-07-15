using Microsoft.Extensions.DependencyInjection;

namespace TestWPFCoreMVVM_HotKey.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
