using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace KbHeatMap.Service
{
    public class KeyboardService
    {
        private const string FileName = "data.json";
        private readonly Dictionary<Keys, int> _pressCount;

        private IKeyboardMouseEvents _globalHook;

        public KeyboardService()
        {
            if (File.Exists(FileName))
            {
                var tempDict = new JavaScriptSerializer()
                    .Deserialize<Dictionary<string, int>>(File.ReadAllText(FileName));

                _pressCount = tempDict.ToDictionary(k =>
                {
                    Enum.TryParse(k.Key, out Keys key);
                    return key;
                }, k => k.Value);
            }
            else
            {
                _pressCount = new Dictionary<Keys, int>();
            }
        }

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

        public void Save()
        {
            File.WriteAllText(FileName, new JavaScriptSerializer().Serialize(
                _pressCount.ToDictionary(k => k.Key.ToString(), k => k.Value)));
        }
    }
}
