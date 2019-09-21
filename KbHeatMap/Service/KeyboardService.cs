using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Colore.Data;
using Colore.Effects.Keyboard;
using Gma.System.MouseKeyHook;
using KbHeatMap.Utils;

namespace KbHeatMap.Service
{
    public class KeyboardService
    {
        private readonly ChromaService _chromaService;
        private const string FileName = "data.json";

        private int _max;
        private readonly Dictionary<Key, int> _pressCount;
        private readonly ColorHeatMap _colorHeatMap = new ColorHeatMap();

        private IKeyboardMouseEvents _globalHook;

        private KeyboardCustom _grid = KeyboardCustom.Create();

        private readonly System.Timers.Timer _saveTimer = new System.Timers.Timer { Interval = 18000000 };

        public KeyboardService(ChromaService chromaService)
        {
            _saveTimer.Elapsed += SaveTimerOnElapsed;
            _chromaService = chromaService;
            _chromaService.SdkInit += ChromaServiceOnSdkInit;
            if (File.Exists(FileName))
            {
                var tempDict = new JavaScriptSerializer()
                    .Deserialize<Dictionary<string, int>>(File.ReadAllText(FileName));

                _pressCount = tempDict.ToDictionary(k =>
                {
                    Enum.TryParse(k.Key, out Key key);
                    return key;
                }, k => k.Value);

                _max = _pressCount.Values.Max();
            }
            else
            {
                _pressCount = new Dictionary<Key, int>();
            }
        }

        private void SaveTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Save();
        }
        private void ChromaServiceOnSdkInit(object sender, SdkInitEvent e)
        {
            if (e.Initialized)
            {
                Task.Delay(TimeSpan.FromMilliseconds(500)).ContinueWith(task => Update());
            }
        }

        private void Update()
        {
            _grid.Set(Color.Blue);

            foreach (var key in _pressCount)
            {
                var color =_colorHeatMap.GetColorForValue(key.Value, _max);
                _grid[key.Key] = color;
            }

            _chromaService.Send(_grid);
        }

        public void Subscribe()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHookOnKeyPress;
            _saveTimer.Start();
        }

        public void Unsubscribe()
        {
            _saveTimer.Stop();
            _globalHook.KeyDown -= GlobalHookOnKeyPress;
            _globalHook.Dispose();
        }

        private void GlobalHookOnKeyPress(object sender, KeyEventArgs e)
        {
            Console.WriteLine($"{e.KeyCode} pressed.");
            var razerKey = Mapping.GetRazerKey(e.KeyCode);
            if (!_pressCount.ContainsKey(razerKey))
            {
                _pressCount.Add(razerKey, 1);
            }
            else
            {
                _pressCount[razerKey]++;
            }

            _max = Math.Max(_max, _pressCount[razerKey]);
            Update();
        }

        public void Save()
        {
            File.WriteAllText(FileName, new JavaScriptSerializer().Serialize(
                _pressCount.ToDictionary(k => k.Key.ToString(), k => k.Value)));
        }
    }
}
