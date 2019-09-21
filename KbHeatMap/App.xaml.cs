using System;
using System.Windows;
using System.Windows.Forms;
using KbHeatMap.Service;

namespace KbHeatMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Notification icon in the task bar.
        /// </summary>
        private NotifyIcon _notifyIcon;

        public readonly ChromaService ChromaService = new ChromaService();
        public readonly KeyboardService KeyboardService;

        public App()
        {
            KeyboardService = new KeyboardService(ChromaService);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Console.WriteLine("Started KbHeatMap");

            _notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = KbHeatMap.Properties.Resources.icon,
                Text = "KbHeatMap"
            };

            CreateContextMenu();

            ChromaService.SdkInit += ChromaServiceOnSdkInit;

            KeyboardService.Subscribe();
            ChromaService.Initialize();
        }

        private void ChromaServiceOnSdkInit(object sender, SdkInitEvent e)
        {
            if (!e.Initialized)
            {
                // Try again...
                System.Threading.Thread.Sleep(5000);
                ChromaService.Initialize();
            }
        }

        /// <summary>
        /// Create the context menu for the notification icon.
        /// </summary>
        private void CreateContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("ReInitialize SDK").Click += ReInitializeSdk;
            menu.Items.Add("Close").Click += (s, e) => ExitApp();

            _notifyIcon.ContextMenuStrip = menu;
        }

        private void ReInitializeSdk(object sender, EventArgs e)
        {
            ChromaService.UnInitialize();
            ChromaService.Initialize();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            ChromaService.UnInitialize();
            KeyboardService.Save();
            KeyboardService.Unsubscribe();
            _notifyIcon.Dispose();
        }

        /// <summary>
        /// Close the windows and remove the notification icon.
        /// </summary>
        private void ExitApp()
        {
            Current.Shutdown();
        }
    }
}
