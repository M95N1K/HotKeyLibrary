using Microsoft.Extensions.DependencyInjection;
using TestWPFCoreMVVM_HotKey.Services.Interfaces;
using HotKeyLibrary;

namespace TestWPFCoreMVVM_HotKey.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
           .AddTransient<IHotKeys, HotKeys>()
        ;
    }
}
