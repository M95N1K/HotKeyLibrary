using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;

namespace HotKeyLibrary
{
    /// <summary>
    /// Класс определяющий нажатие клавиш
    /// </summary>
    public class HotKeys : IHotKeys
    {
        /// <summary>
        /// Событие происходящее во время нажатия горячей клавишы (-1 клавиша не нажата)
        /// </summary>
        public event Action<List<int>> OnHotKey;

        /// <summary>
        /// Событие происходящее во время нажатия горячей клавишы (-1 клавиша не нажата)
        /// </summary>
        public event Action<string> OnHotKeyString;

        //private bool _start = false;

        private readonly Timer _timer = new();
        /// <summary> Показывает запущен ли таймер </summary>
        public bool IsEnable { get => _timer?.Enabled ?? false; }

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(Int32 i);

        private List<int> PressedKey()
        {

            List<int> keys = new();
            for (int i = 1; i < 256; i++)
            {
                if (GetAsyncKeyState(i) != 0)
                    keys.Add(i);
            }

            return keys;
        }

        /// <summary>
        /// Получение нажатых клавишь (для отслеживания запускается в цикле)
        /// </summary>
        /// <returns></returns>
        public async Task GetKeyAsync()
        {
            await Task.Run(() => _timer_Elapsed(null, null));
        }

        /// <summary>
        /// Стартует таймер определения нажатий клавишы
        /// </summary>
        public void StartHK()
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = 1;
            _timer.Start();
        }

        int c = 0;
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var i = PressedKey();
            if (i.Count == 0)
            {
                c = 0;
                _oldHotKey = new();
                List<int> key = new List<int>() { -1 };
                OnHotKey?.Invoke(key);
                OnHotKeyString?.Invoke(KeysToString(key));
                return;
            }
            if (c > 0 && i.Count < c)
                return;
            if (i != null)
            {
                GetSingleHotKey(i);
                c = i.Count;
            }

        }

        private List<int> _oldHotKey = new();
        private void GetSingleHotKey(List<int> key)
        {
            //List<int> result = new();

            bool equals = true;
            if (_oldHotKey.Count == key.Count)
            {
                for (int c = 0; c < _oldHotKey.Count; c++)
                {
                    if (_oldHotKey[c] != key[c])
                        equals = false;
                }
                if (equals)
                    return;
            }

            _oldHotKey = key;
            OnHotKey?.Invoke(key);
            
            OnHotKeyString?.Invoke(KeysToString(key));
            //return key;
        }

        private static string KeysToString(List<int> key)
        {
            string keyString = "";
            List<int> _key = new(key);
            
            foreach (var item in _key)
            {
                if (item > 159 && item < 166)
                    continue;
                if (item == 16)
                {
                    var mod = _key.FirstOrDefault(o => o == 160 || o == 161);
                    switch (mod)
                    {
                        case 160:
                            keyString += "LeftShift  + ";
                            break;
                        case 161:
                            keyString += "RightShift  + ";
                            break;
                        default:
                            keyString += "Shift  + ";
                            break;
                    }
                }
                else if (item == 17)
                {
                    var mod = _key.FirstOrDefault(o => o == 162 || o == 163);
                    switch (mod)
                    {
                        case 162:
                            keyString += "LeftCtrl  + ";
                            break;
                        case 163:
                            keyString += "RightCtrl  + ";
                            break;
                        default:
                            keyString += "Ctrl  + ";
                            break;
                    }
                }
                else if (item == 18)
                {
                    var mod = _key.FirstOrDefault(o => o == 164 || o == 165);
                    switch (mod)
                    {
                        case 164:
                            keyString += "LeftAlt  + ";
                            break;
                        case 165:
                            keyString += "RightAlt  + ";
                            break;
                        default:
                            keyString += "Alt  + ";
                            break;
                    }
                }
                else
                {
                    var conKey = (ConsoleKey)item;
                    keyString += conKey + " + ";
                }
            }
            if(keyString.Length > 3)
                keyString = keyString.Remove(keyString.Length - 3);

            return keyString;
        }

        /// <summary>
        /// Остановка таймера определяющего нажатия клавишь
        /// </summary>
        public void StopHK()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Деструктор класса
        /// </summary>
        ~HotKeys()
        {
            StopHK();
            _timer.Dispose();
        }
    }
}
