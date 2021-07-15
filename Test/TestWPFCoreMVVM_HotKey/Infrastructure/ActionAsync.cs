using System.Threading.Tasks;

namespace TestWPFCoreMVVM_HotKey.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
