using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace KbHeatMap.Service
{
    public class KeyboardService
    {
        private Dictionary<Keys, int> _pressCount = new Dictionary<Keys, int>();

        private IKeyboardMouseEvents  _globalHook;

        public void Subscribe()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHookOnKeyPress;
        }

        public void Unsubscribe()
        {
            _globalHook.KeyDown -= GlobalHookOnKeyPress;
            _globalHook.Dispose();
        }

        private void GlobalHookOnKeyPress(object sender, KeyEventArgs e)
        {
            Console.WriteLine($"{e.KeyCode} pressed.");

            if (!_pressCount.ContainsKey(e.KeyCode))
            {
                _pressCount.Add(e.KeyCode, 1);
            }
            else
            {
                _pressCount[e.KeyCode]++;
            }
        }
    }
}
