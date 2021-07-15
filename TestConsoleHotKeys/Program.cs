using HotKeyLibrary;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsoleHotKeys
{
    class Program
    {

        private static bool _run = true;

        private static readonly HotKeys _hotKeys = new();
        static async Task Main(string[] args)
        {

            _hotKeys.OnHotKey += HotKeys_OnHotKey;
            while (_run)
            {
                await _hotKeys.GetKeyAsync();
                Thread.Sleep(1);
            }
        }

        private static void HotKeys_OnHotKey(List<int> key)
        {

            Console.Clear();
            for (int index = 0; index < key.Count; index++)
            {
                int item = key[index];
                Console.WriteLine(item.ToString());
            }

            if (key[0] == 81)
            {
                _hotKeys.StopHK();
                _run = false;
            }

        }
    }
}
