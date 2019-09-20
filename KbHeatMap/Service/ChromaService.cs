﻿using System;
using System.Collections.Generic;
using System.Timers;
using KbHeatMap.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace KbHeatMap.Service
{
    public class ChromaService
    {
        public static readonly string MainUrl = "http://localhost:54235/razer/chromasdk";

        public static readonly Dictionary<string, object> AppInfo = new Dictionary<string, object>
        {
            {"title", "Keyboard Heat Map"},
            {"description", "Keyboard Heat Map"},
            {
                "author", new Dictionary<string, string>
                {
                    {"name", "Long Tran"},
                    {"contact", "https://github.com/Longi94/KbHeatMap/"}
                }
            },
            {"device_supported", new[] {"keyboard"}},
            {"category", "application"}
        };

        /// <summary>
        /// Event fired when the SDK is initialized or uninitialized.
        /// </summary>
        public event EventHandler<SdkInitEvent> SdkInit;
        public event EventHandler<SdkUnInitEvent> SdkUnInit;

        public bool Initialized { get; private set; }

        private readonly Timer _heartbeatTimer = new Timer { Interval = 10000 };

        private readonly RestClient _mainClient = new RestClient(MainUrl);

        public string Uri { get; private set; }

        private RestClient _client;

        private readonly uint[][] _colorEffectBuffer = new uint[Constants.KbRows][];
        private readonly uint[][] _keyEffectBuffer = new uint[Constants.KbRows][];

        public ChromaService()
        {
            _heartbeatTimer.Elapsed += Heartbeat;

            for (int i = 0; i < Constants.KbRows; i++)
            {
                _colorEffectBuffer[i] = new uint[Constants.KbColumns];
                _keyEffectBuffer[i] = new uint[Constants.KbColumns];
            }
        }

        /// <summary>
        /// Initialized the Chroma SDK if not initialized already.
        /// </summary>
        public void Initialize()
        {
            if (Initialized) return;

            Console.WriteLine("Initializing Chroma SDK...");

            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(AppInfo);

            _mainClient.ExecuteAsync<InitResponse>(request, response =>
            {
                if (response.IsSuccessful && response.Data.Uri != null)
                {
                    Uri = response.Data.Uri;
                    _client = new RestClient(Uri);
                    Initialized = true;
                    _heartbeatTimer.Start();
                    Console.WriteLine("Initialized Chroma SDK");

                    SdkInit?.Invoke(this, new SdkInitEvent { Initialized = Initialized });
                }
                else
                {
                    Console.WriteLine("Failed to initialize SDK");
                    Console.WriteLine(response.ErrorMessage);
                }
            });
        }

        /// <summary>
        /// Uninitialized the Chroma SDK. Synapse (or other apps) will assume control of the lights.
        /// </summary>
        public void UnInitialize()
        {
            if (!Initialized) return;

            Console.WriteLine("Uninitializing Chroma SDK...");
            _heartbeatTimer.Stop();

            var request = new RestRequest(Method.DELETE);

            _client.ExecuteAsync<UnInitResponse>(request, response =>
            {
                Initialized = false;

                SdkUnInit?.Invoke(this, new SdkUnInitEvent());
                if (response.IsSuccessful && response.Data.Result == 0)
                {
                    Console.WriteLine("Uninitialized Chroma SDK");
                }
                else
                {
                    Console.WriteLine("Failed to uninitialize SDK");
                    Console.WriteLine(response.IsSuccessful ? $"Returned code: {response.Data.Result}" : response.ErrorMessage);
                }
            });
        }

        /// <summary>
        /// Send a heartbeat message to the chroma sdk to keep the connection alive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (!Initialized) return;

            var request = new RestRequest("/heartbeat", Method.PUT);
            var response = _client.Execute<HeartbeatResponse>(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine($"Chroma SDK heartbeat {response.Data.Tick}");
            }
            else
            {
                Console.WriteLine("Heartbeat failed");
                Console.WriteLine(response.ErrorMessage);
            }
        }

        public void Send(Grid colorGrid, Grid keyGrid = null, bool apply = false)
        {
            if (!Initialized)
            {
                return;
            }

            if (colorGrid == null)
            {
                throw new ArgumentNullException(nameof(colorGrid));
            }

            var body = new Dictionary<string, object>();
            colorGrid.Serialize(_colorEffectBuffer);

            if (keyGrid == null)
            {
                body["effect"] = "CHROMA_CUSTOM";
                body["param"] = _colorEffectBuffer;
            }
            else
            {
                body["effect"] = "CHROMA_CUSTOM_KEY";

                keyGrid.Serialize(_keyEffectBuffer);
                body["param"] = new[] { _colorEffectBuffer, _keyEffectBuffer };
            }

            var request = new RestRequest("/keyboard", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(body);

            _client.ExecuteAsync<EffectResponse>(request, response =>
            {
                if (response.IsSuccessful)
                {
                    if (apply)
                    {
                        ApplyEffect(response.Data.Id);
                    }
                }
                else
                {
                    Console.WriteLine("Failed to set custom effect");
                    Console.WriteLine(response.ErrorMessage);
                }
            });
        }

        public void ApplyEffect(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var request = new RestRequest("/effect", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new Dictionary<string, string> { { "id", id } });

            _client.ExecuteAsync<object>(request, response =>
            {
                if (!response.IsSuccessful)
                {
                    Console.WriteLine($"Failed to apply effect {id}");
                    Console.WriteLine(response.ErrorMessage);
                }
            });
        }
    }

    public class SdkInitEvent : EventArgs
    {
        public bool Initialized;
    }

    public class SdkUnInitEvent : EventArgs
    {
    }

    internal class InitResponse
    {
        [DeserializeAs(Name = "session")]
        public int Session { get; set; }

        [DeserializeAs(Name = "uri")]
        public string Uri { get; set; }
    }

    internal class UnInitResponse
    {
        [DeserializeAs(Name = "result")]
        public int Result { get; set; }
    }

    internal class HeartbeatResponse
    {
        [DeserializeAs(Name = "tick")]
        public int Tick { get; set; }
    }

    internal class EffectResponse
    {
        [DeserializeAs(Name = "result")]
        public int Result { get; set; }

        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
    }
}
