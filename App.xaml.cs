using System;
using System.Threading;
using System.Windows;

namespace PurpleNotebook
{
    public partial class App : Application
    {
        private static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNewInstance;
            mutex = new Mutex(true, "PurpleNotebookMutex", out isNewInstance);

            if (!isNewInstance)
            {
                MessageBox.Show("Purple Notebook is already running.", "Duplicate Launch", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown();
                return;
            }

            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();
            Console.WriteLine("[INFO] MainWindow launched");
        }
    }
}

