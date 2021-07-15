using System;
using System.Windows;
using TestWPFCoreMVVM_HotKey.Infrastructure.Commands.Base;

namespace TestWPFCoreMVVM_HotKey.Infrastructure.Commands
{
    class ColseWindow : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActivedWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p) => GetWindow(p)?.Close();
    }
}
