using TestWPFCoreMVVM_HotKey.Services.Interfaces;
using TestWPFCoreMVVM_HotKey.ViewModels.Base;
using TestWPFCoreMVVM_HotKey.Infrastructure.Commands;
using HotKeyLibrary;
using System.Collections.Generic;

namespace TestWPFCoreMVVM_HotKey.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        private readonly IHotKeys _hotKeys;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        #region HotKey
        private List<int> _hotKey = new();
        public List<int> HotKey { get => _hotKey; set => Set(ref _hotKey, value); }
        #endregion

        #region Commands

        #region StartCommand
        public LambdaCommand StartCommand { get; }
        private void StartOnExecuted(object obj) 
        {
            _hotKeys.StartHK();
        }
        private bool StartCanExecute(object obj) => true;
        #endregion

        #region StopCommand
        public LambdaCommand StopCommand { get; }
        private void StopOnExecuted(object obj)
        {
            _hotKeys.StopHK();
        }
        private bool StopCanExecute(object obj) => true;
        #endregion

        #endregion

        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService, IHotKeys hotKeys)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
            _hotKeys = hotKeys;

            _hotKeys.OnHotKey += _hotKeys_OnHotKey;
            _hotKeys.OnHotKeyString += _hotKeys_OnHotKeyString;

            StartCommand = new LambdaCommand(StartOnExecuted, StartCanExecute);
            StopCommand = new LambdaCommand(StopOnExecuted, StopCanExecute);
        }

        private void _hotKeys_OnHotKeyString(string key)
        {
            Title = key;
        }

        private void _hotKeys_OnHotKey(System.Collections.Generic.List<int> key)
        {
            HotKey = key;
        }
    }
}
