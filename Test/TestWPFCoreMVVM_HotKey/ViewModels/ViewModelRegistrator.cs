using Microsoft.Extensions.DependencyInjection;

namespace TestWPFCoreMVVM_HotKey.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
           .AddSingleton<MainWindowViewModel>()
        ;
    }
}