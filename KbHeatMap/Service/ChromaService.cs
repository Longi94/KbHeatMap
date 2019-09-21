using System;
using Colore;
using Colore.Effects.Keyboard;

namespace KbHeatMap.Service
{
    public class ChromaService
    {
        /// <summary>
        /// Event fired when the SDK is initialized or uninitialized.
        /// </summary>
        public event EventHandler<SdkInitEvent> SdkInit;

        public bool Initialized { get; private set; }

        private IChroma _chroma;

        public void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            ColoreProvider.CreateNativeAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    _chroma = task.Result;
                    Initialized = _chroma.Initialized;
                }
                SdkInit?.Invoke(this, new SdkInitEvent { Initialized = Initialized });
            });
        }

        public void UnInitialize()
        {
            if (!Initialized)
            {
                return;
            }

            _chroma?.UninitializeAsync();
            Initialized = false;
        }

        public void Send(KeyboardCustom colorGrid)
        {
            if (!Initialized)
            {
                return;
            }

            _chroma?.Keyboard.SetCustomAsync(colorGrid);
        }
    }

    public class SdkInitEvent : EventArgs
    {
        public bool Initialized;
    }
}
