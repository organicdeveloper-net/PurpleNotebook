using System;
using System.IO;
using System.Media;
using System.Windows;

namespace PurpleNotebook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BootSequence();
        }

        #region Window Events

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double aspectRatio = 0.75;
            this.Width = this.Height * aspectRatio;
        }

        #endregion

        #region Boot Sequence

        private void BootSequence()
        {
            PlayBootSound();
            LogBootTimestamp();
        }

        private void PlayBootSound()
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "boot.wav");

            if (File.Exists(soundPath))
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Boot sound playback failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("[WARN] Boot sound file not found.");
            }
        }

        private void LogBootTimestamp()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[BOOT] Notebook launched at {timestamp}");
        }

        #endregion
    }
}
