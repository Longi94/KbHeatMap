using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using KbHeatMap.Model;
using KbHeatMap.Utils;
using Constants = KbHeatMap.Razer.Constants;

namespace KbHeatMap.Service
{
    public class KeyboardService
    {
        private readonly ChromaService _chromaService;
        private const string FileName = "data.json";
        private readonly Dictionary<Constants.Key, int> _pressCount;

        private IKeyboardMouseEvents _globalHook;

        private Grid _grid = new Grid();

        public KeyboardService(ChromaService chromaService)
        {
            _chromaService = chromaService;
            _chromaService.SdkInit += ChromaServiceOnSdkInit;
            if (File.Exists(FileName))
            {
                var tempDict = new JavaScriptSerializer()
                    .Deserialize<Dictionary<string, int>>(File.ReadAllText(FileName));

                _pressCount = tempDict.ToDictionary(k =>
                {
                    Enum.TryParse(k.Key, out Constants.Key key);
                    return key;
                }, k => k.Value);
            }
            else
            {
                _pressCount = new Dictionary<Constants.Key, int>();
            }
        }

        private void ChromaServiceOnSdkInit(object sender, SdkInitEvent e)
        {

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
            var razerKey = Mapping.GetRazerKey(e.KeyCode);
            if (!_pressCount.ContainsKey(razerKey))
            {
                _pressCount.Add(razerKey, 1);
            }
            else
            {
                _pressCount[razerKey]++;
            }

            _grid.SetAll(ChromaColor.Black);
            _grid.Set(razerKey, ChromaColor.White);
            _chromaService.Send(_grid, apply:true);
        }

        public void Save()
        {
            File.WriteAllText(FileName, new JavaScriptSerializer().Serialize(
                _pressCount.ToDictionary(k => k.Key.ToString(), k => k.Value)));
        }
    }
}
