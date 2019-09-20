﻿using System.Windows;
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
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = KbHeatMap.Properties.Resources.icon,
                Text = "KbHeatMap"
            };

            CreateContextMenu();

            ChromaService.Initialize();
        }

        /// <summary>
        /// Create the context menu for the notification icon.
        /// </summary>
        private void CreateContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Close").Click += (s, e) => ExitApp();

            _notifyIcon.ContextMenuStrip = menu;
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            ChromaService.UnInitialize();
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
