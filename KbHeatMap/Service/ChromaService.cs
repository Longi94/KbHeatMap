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

        public event EventHandler<SdkUnInitEvent> SdkUnInit;

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

                SdkInit?.Invoke(this, new SdkInitEvent {Initialized = Initialized});
            });
        }

        public void UnInitialize()
        {
            if (!Initialized)
            {
                return;
            }

            if (_chroma == null)
            {
                SdkUnInit?.Invoke(this, new SdkUnInitEvent());
            }
            else
            {
                _chroma.UninitializeAsync().ContinueWith(task => SdkUnInit?.Invoke(this, new SdkUnInitEvent()));
            }

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

    public class SdkUnInitEvent : EventArgs
    {
    }
}
