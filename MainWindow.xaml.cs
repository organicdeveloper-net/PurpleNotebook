using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string themeName = selectedItem.Content.ToString();
                Brush backgroundBrush = GetThemeBrush(themeName);
                this.Background = backgroundBrush;

                Console.WriteLine($"[THEME] Applied theme: {themeName}");
            }
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

        #region Theme Logic

        private Brush GetThemeBrush(string themeName)
        {
            switch (themeName)
            {
                case "Lilac":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7C33CB"));
                case "Lavender":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A383E8"));
                case "Smoke":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62679C"));
                case "Grape":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8648FF"));
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        #endregion
    }
}
