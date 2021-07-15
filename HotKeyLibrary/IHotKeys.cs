
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HotKeyLibrary.HotKeys;

namespace HotKeyLibrary
{
    /// <summary>
    /// Интерфейс класса определяющий нажатие клавиш
    /// </summary>
    public interface IHotKeys
    {
        /// <summary> Показывает запущен ли таймер </summary>
        public bool IsEnable { get; }

        /// <summary>
        /// Событие происходящее во время нажатия горячей клавишы передающее массив кодов нажатых клавишь
        /// </summary>
        public event Action<List<int>> OnHotKey;

        /// <summary>
        /// Событие происходящее во время нажатия горячей клавишы передающее нажатые клавиши в string
        /// </summary>
        public event Action<string> OnHotKeyString;

        /// <summary>
        /// Получение нажатых клавишь (для отслеживания запускается в цикле)
        /// </summary>
        /// <returns></returns>
        public Task GetKeyAsync();

        /// <summary>
        /// Стартует таймер определения нажатий клавишы
        /// </summary>
        public void StartHK();

        /// <summary>
        /// Остановка таймера определяющего нажатия клавишь
        /// </summary>
        public void StopHK();
    }
}
