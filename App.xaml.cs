using System;
using System.Windows;

namespace PurpleNotebook
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Console.WriteLine("[INFO] MainWindow launched from App.xaml.cs");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch MainWindow:\n" + ex.Message, "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"[ERROR] App startup failed: {ex.Message}");
            }
        }
    }
}
