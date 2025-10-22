using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;

namespace PurpleNotebook
{
    public partial class SplashScreen : Window
    {
        #region Constructor

        public SplashScreen()
        {
            InitializeComponent();
            Loaded += OnSplashLoaded;
        }

        #endregion

        #region Event Handlers

        private async void OnSplashLoaded(object sender, RoutedEventArgs e)
        {
            PlaySplashSound();
            await Task.Delay(TimeSpan.FromSeconds(2));
            TransitionToMainWindow();
        }

        #endregion

        #region Sound Logic

        private void PlaySplashSound()
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "splash.wav");

            if (!File.Exists(soundPath))
            {
                Console.WriteLine("[WARN] splash.wav not found in Assets folder.");
                return;
            }

            try
            {
                new SoundPlayer(soundPath).Play();
                Console.WriteLine($"[SPLASH] Played splash.wav at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Splash sound playback failed: {ex.Message}");
            }
        }

        #endregion

        #region Transition Logic

        private void TransitionToMainWindow()
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void SplashVideo_MediaEnded(object sender, RoutedEventArgs e)
 {
        // Transition to MainWindow or perform next action
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();
        }

        #endregion
    }
}
